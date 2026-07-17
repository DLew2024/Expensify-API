using System.ComponentModel.DataAnnotations;
using Expensify.API.Configurations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Functions;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models;
using Expensify.DataAccessLayer.Enums;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Expensify.API.ServiceClasses;

public class AuthService(
    ApplicationDbContext context,
    IJwtService jwtService,
    IPasswordService passwordService,
    ISecurityService securityService,
    IEmailService emailService,
    IOptions<FrontendSettings> frontendOptions,
    IOptions<JwtSettings> jwtSettings
) : IAuthService
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly ISecurityService _securityService = securityService;
    private readonly IEmailService _emailService = emailService;
    private readonly ApplicationDbContext _context = context;
    private readonly FrontendSettings _frontendSettings = frontendOptions.Value;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<Result<bool>> ChangePassword(
        Guid authenticatedUserId,
        ChangePasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        if (
            ValidationHelpers.HasEmptyOrWhiteSpace(
                request.CurrentPassword,
                request.NewPassword,
                request.ConfirmNewPassword
            )
        )
        {
            return new Result<bool>(new ValidationException("All password fields are required."));
        }

        if (request.NewPassword != request.ConfirmNewPassword)
        {
            return new Result<bool>(new ValidationException("New passwords do not match."));
        }

        if (request.NewPassword.Length < ValidationHelpers.PasswordLengthMinimum)
        {
            return new Result<bool>(
                new ValidationException(
                    $"Password must be at least {ValidationHelpers.PasswordLengthMinimum} characters long."
                )
            );
        }

        var foundUser = await _context.Users.FirstOrDefaultAsync(
            user => user.Id == authenticatedUserId,
            cancellationToken
        );

        if (foundUser is null)
        {
            return new Result<bool>(
                new UnauthorizedAccessException("The authenticated user is invalid.")
            );
        }

        var currentPasswordIsValid = _passwordService.VerifyPassword(
            request.CurrentPassword,
            foundUser.Password
        );

        if (!currentPasswordIsValid)
        {
            return new Result<bool>(new ValidationException("The current password is incorrect."));
        }

        var newPasswordMatchesCurrentPassword = _passwordService.VerifyPassword(
            request.NewPassword,
            foundUser.Password
        );

        if (newPasswordMatchesCurrentPassword)
        {
            return new Result<bool>(
                new ValidationException(
                    "The new password must be different from the current password."
                )
            );
        }

        foundUser.Password = _passwordService.HashPassword(request.NewPassword);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<bool>(true);
    }

    public async Task<Result<bool>> EmailVerification(
        EmailVerificationDTO request,
        CancellationToken cancellationToken
    )
    {
        if (ValidationHelpers.HasEmptyOrWhiteSpace(request.Token))
        {
            return new Result<bool>(new ValidationException("Verification token is required."));
        }

        var tokenHash = _securityService.HashToken(request.Token);
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var verificationToken = await _context.EmailVerificationTokens.FirstOrDefaultAsync(
                token =>
                    token.TokenHash == tokenHash
                    && token.UsedAt == null
                    && !token.IsRevoked
                    && token.ExpiresAt > currentTimestamp,
                cancellationToken
            );

            if (verificationToken is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<bool>(
                    new ValidationException(
                        "The email verification token is invalid or has expired."
                    )
                );
            }

            var foundUser = await _context.Users.FirstOrDefaultAsync(
                user => user.Id == verificationToken.UserId,
                cancellationToken
            );

            if (foundUser is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<bool>(
                    new ValidationException(
                        "The email verification token is invalid or has expired."
                    )
                );
            }

            if (foundUser.IsEmailVerified)
            {
                verificationToken.UsedAt = currentTimestamp;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Result<bool>(true);
            }

            foundUser.IsEmailVerified = true;
            foundUser.EmailVerifiedAt = currentTimestamp;

            verificationToken.UsedAt = currentTimestamp;
            verificationToken.UpdatedDate = currentTimestamp;
            verificationToken.LastUpdatedBy = foundUser.Id;

            var otherActiveTokens = await _context
                .EmailVerificationTokens.Where(token =>
                    token.UserId == foundUser.Id
                    && token.Id != verificationToken.Id
                    && token.UsedAt == null
                    && !token.IsRevoked
                )
                .ToListAsync(cancellationToken);

            foreach (var token in otherActiveTokens)
            {
                token.IsRevoked = true;
                token.UpdatedDate = currentTimestamp;
                token.LastUpdatedBy = foundUser.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new Result<bool>(true);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<bool>(
                new ValidationException(
                    "The email verification token is invalid or has already been used."
                )
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<bool>(ex);
        }
    }

    public async Task<Result<bool>> ForgotPassword(
        ForgotPasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        var normalizedEmail = ValidationHelpers.Normalize(request.Email);

        if (!ValidationHelpers.IsValidEmail(normalizedEmail))
        {
            return new Result<bool>(
                new ValidationException("Passed email is not in correct format.")
            );
        }

        var foundUser = await _context.Users.FirstOrDefaultAsync(
            user => user.Email == normalizedEmail,
            cancellationToken
        );

        if (foundUser is null)
        {
            return new Result<bool>(true);
        }

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var rawToken = _securityService.GenerateSecureToken();
        var tokenHash = _securityService.HashToken(rawToken);

        PasswordResetToken passwordResetToken;

        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var existingTokens = await _context
                .PasswordResetTokens.Where(token =>
                    token.UserId == foundUser.Id && token.UsedAt == null && !token.IsRevoked
                )
                .ToListAsync(cancellationToken);

            foreach (var existingToken in existingTokens)
            {
                existingToken.IsRevoked = true;
                existingToken.UpdatedDate = now;
                existingToken.LastUpdatedBy = foundUser.Id;
            }

            passwordResetToken = new PasswordResetToken
            {
                UserId = foundUser.Id,
                TokenHash = tokenHash,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds(),
                CreatedBy = foundUser.Id,
                CreateDate = now,
            };

            _context.PasswordResetTokens.Add(passwordResetToken);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Result<bool>(ex);
        }

        var resetLink =
            $"{_frontendSettings.BaseUrl.TrimEnd('/')}/reset-password?token={Uri.EscapeDataString(rawToken)}";

        try
        {
            await _emailService.SendPasswordResetEmail(
                foundUser.Email,
                resetLink,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            passwordResetToken.IsRevoked = true;
            passwordResetToken.UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            passwordResetToken.LastUpdatedBy = foundUser.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<bool>(ex);
        }

        return new Result<bool>(true);
    }

    public async Task<Result<UserResponseDTO>> GetUserInfo(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var userInfo = await _context
                .Users.AsNoTracking()
                .Where(user => user.Id == id)
                .Select(UserResponseDTO.Projection)
                .FirstOrDefaultAsync(cancellationToken);

            if (userInfo is null)
            {
                return new Result<UserResponseDTO>(
                    new EntityNotFoundException($"User with id was not found: {id}")
                );
            }

            return new Result<UserResponseDTO>(userInfo);
        }
        catch (Exception ex)
        {
            return new Result<UserResponseDTO>(ex);
        }
    }

    public async Task<Result<UserTokenResponseDTO>> LoginUser(
        LoginUserDTO request,
        CancellationToken cancellationToken
    )
    {
        var normalizedEmail = ValidationHelpers.Normalize(request.Email);

        try
        {
            var foundUser = await _context
                .Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == normalizedEmail, cancellationToken);

            if (
                foundUser is null
                || !_passwordService.VerifyPassword(request.Password, foundUser.Password)
            )
            {
                return new Result<UserTokenResponseDTO>(
                    new UnauthorizedAccessException("Invalid email or password.")
                );
            }

            var token = _jwtService.GenerateToken(foundUser);

            return new Result<UserTokenResponseDTO>(
                new UserTokenResponseDTO
                {
                    User = UserResponseDTO.FromUser(foundUser),
                    Token = token,
                }
            );
        }
        catch (Exception ex)
        {
            return new Result<UserTokenResponseDTO>(ex);
        }
    }

    public async Task<Result<bool>> LogoutUser(
        Guid currentUserId,
        LogoutUserDTO request,
        CancellationToken cancellationToken
    )
    {
        if (currentUserId == Guid.Empty)
        {
            return new Result<bool>(
                new UnauthorizedAccessException("The authenticated user is invalid.")
            );
        }

        return request.Scope switch
        {
            LogoutScope.CurrentDevice => await LogoutCurrentDevice(
                currentUserId,
                request.RefreshToken,
                cancellationToken
            ),

            LogoutScope.AllDevices => await LogoutAllDevices(currentUserId, cancellationToken),

            _ => new Result<bool>(new ValidationException("Invalid logout scope.")),
        };
    }

    private async Task<Result<bool>> LogoutCurrentDevice(
        Guid currentUserId,
        string? refreshToken,
        CancellationToken cancellationToken
    )
    {
        if (ValidationHelpers.HasEmptyOrWhiteSpace(refreshToken) || refreshToken == null)
        {
            return new Result<bool>(
                new ValidationException(
                    "Refresh token is required when logging out the current device."
                )
            );
        }

        var tokenHash = _securityService.HashToken(refreshToken);
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var foundRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(
            token =>
                token.UserId == currentUserId
                && token.TokenHash == tokenHash
                && !token.IsRevoked
                && token.RevokedAt == null,
            cancellationToken
        );

        // Logout is idempotent. An unknown or already revoked token is considered logged out.
        if (foundRefreshToken is null)
        {
            return new Result<bool>(true);
        }

        foundRefreshToken.IsRevoked = true;
        foundRefreshToken.RevokedAt = currentTimestamp;
        foundRefreshToken.RevocationReason = "User logout";
        foundRefreshToken.LastUpdatedBy = currentUserId;
        foundRefreshToken.UpdatedDate = currentTimestamp;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new Result<bool>(true);
        }
        catch (DbUpdateConcurrencyException)
        {
            return new Result<bool>(true);
        }
        catch (Exception ex)
        {
            return new Result<bool>(ex);
        }
    }

    private async Task<Result<bool>> LogoutAllDevices(
        Guid currentUserId,
        CancellationToken cancellationToken
    )
    {
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        try
        {
            await _context
                .RefreshTokens.Where(token =>
                    token.UserId == currentUserId && !token.IsRevoked && token.RevokedAt == null
                )
                .ExecuteUpdateAsync(
                    updates =>
                        updates
                            .SetProperty(token => token.IsRevoked, true)
                            .SetProperty(token => token.RevokedAt, currentTimestamp)
                            .SetProperty(
                                token => token.RevocationReason,
                                "User logout from all devices"
                            )
                            .SetProperty(token => token.LastUpdatedBy, currentUserId)
                            .SetProperty(token => token.UpdatedDate, currentTimestamp),
                    cancellationToken
                );

            return new Result<bool>(true);
        }
        catch (Exception ex)
        {
            return new Result<bool>(ex);
        }
    }

    public async Task<Result<RefreshTokenResponseDTO>> RefreshTokens(
        RefreshTokensDTO request,
        CancellationToken cancellationToken
    )
    {
        if (ValidationHelpers.HasEmptyOrWhiteSpace(request.RefreshToken))
        {
            return new Result<RefreshTokenResponseDTO>(
                new ValidationException("Refresh token is required.")
            );
        }

        var refreshTokenHash = _securityService.HashToken(request.RefreshToken);
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var existingRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(
                token => token.TokenHash == refreshTokenHash,
                cancellationToken
            );

            if (existingRefreshToken is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<RefreshTokenResponseDTO>(
                    new UnauthorizedAccessException("The refresh token is invalid or has expired.")
                );
            }

            if (existingRefreshToken.IsRevoked || existingRefreshToken.RevokedAt != null)
            {
                await RevokeAllRefreshTokens(
                    existingRefreshToken.UserId,
                    "Previously revoked refresh token was reused.",
                    currentTimestamp,
                    cancellationToken
                );

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Result<RefreshTokenResponseDTO>(
                    new UnauthorizedAccessException("The refresh token is invalid or has expired.")
                );
            }

            if (existingRefreshToken.ExpiresAt <= currentTimestamp)
            {
                existingRefreshToken.IsRevoked = true;
                existingRefreshToken.RevokedAt = currentTimestamp;
                existingRefreshToken.RevocationReason = "Refresh token expired.";
                existingRefreshToken.LastUpdatedBy = existingRefreshToken.UserId;
                existingRefreshToken.UpdatedDate = currentTimestamp;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Result<RefreshTokenResponseDTO>(
                    new UnauthorizedAccessException("The refresh token is invalid or has expired.")
                );
            }

            var foundUser = await _context.Users.FirstOrDefaultAsync(
                user => user.Id == existingRefreshToken.UserId,
                cancellationToken
            );

            if (foundUser is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<RefreshTokenResponseDTO>(
                    new UnauthorizedAccessException("The refresh token is invalid or has expired.")
                );
            }

            var newRawRefreshToken = _securityService.GenerateSecureToken();
            var newRefreshTokenHash = _securityService.HashToken(newRawRefreshToken);

            var newRefreshToken = new RefreshToken
            {
                UserId = foundUser.Id,
                TokenHash = newRefreshTokenHash,
                ExpiresAt = DateTimeOffset
                    .UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
                    .ToUnixTimeSeconds(),
                IsRevoked = false,
                CreatedBy = foundUser.Id,
                CreateDate = currentTimestamp,
            };

            existingRefreshToken.IsRevoked = true;
            existingRefreshToken.RevokedAt = currentTimestamp;
            existingRefreshToken.RevocationReason = "Replaced during refresh-token rotation.";
            existingRefreshToken.LastUpdatedBy = foundUser.Id;
            existingRefreshToken.UpdatedDate = currentTimestamp;

            _context.RefreshTokens.Add(newRefreshToken);

            var newAccessToken = _jwtService.GenerateToken(foundUser);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new Result<RefreshTokenResponseDTO>(
                new RefreshTokenResponseDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRawRefreshToken,
                    ExpiresIn = _jwtSettings.ExpirationMinutes * 60,
                }
            );
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<RefreshTokenResponseDTO>(
                new UnauthorizedAccessException(
                    "The refresh token is invalid or has already been used."
                )
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<RefreshTokenResponseDTO>(ex);
        }
    }

    private async Task RevokeAllRefreshTokens(
        Guid userId,
        string reason,
        long currentTimestamp,
        CancellationToken cancellationToken
    )
    {
        var activeRefreshTokens = await _context
            .RefreshTokens.Where(token =>
                token.UserId == userId && !token.IsRevoked && token.RevokedAt == null
            )
            .ToListAsync(cancellationToken);

        foreach (var token in activeRefreshTokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = currentTimestamp;
            token.RevocationReason = reason;
            token.LastUpdatedBy = userId;
            token.UpdatedDate = currentTimestamp;
        }
    }

    public async Task<Result<UserTokenResponseDTO>> RegisterUser(
        RegisterUserDTO request,
        CancellationToken cancellationToken
    )
    {
        if (
            ValidationHelpers.HasEmptyOrWhiteSpace(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            )
        )
        {
            return new Result<UserTokenResponseDTO>(
                new ValidationException("All required fields must be provided.")
            );
        }

         var normalizedEmail = ValidationHelpers.Normalize(request.Email);

        if (!ValidationHelpers.IsValidEmail(normalizedEmail))
        {
            return new Result<UserTokenResponseDTO>(
                new ValidationException("Passed email is not in the correct format.")
            );
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var userExists = await _context.Users.AnyAsync(
                user => user.Email == normalizedEmail,
                cancellationToken
            );

            if (userExists)
            {
                return new Result<UserTokenResponseDTO>(
                    new ConflictException("A user with this email already exists.")
                );
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = $"{request.FirstName.Trim()} {request.LastName.Trim()}",
                Email = normalizedEmail,
                Password = _passwordService.HashPassword(request.Password),
                ProfileImageUrl = request.ProfileImageURl,
            };

            var token = _jwtService.GenerateToken(newUser);

            var response = new UserTokenResponseDTO
            {
                User = UserResponseDTO.FromUser(newUser),
                Token = token,
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new Result<UserTokenResponseDTO>(response);
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<UserTokenResponseDTO>(
                new ConflictException("A user with this email already exists.", ex)
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<UserTokenResponseDTO>(ex);
        }
    }

    public async Task<Result<bool>> ResetPassword(
        ResetPasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        if (ValidationHelpers.HasEmptyOrWhiteSpace(request.Token))
        {
            return new Result<bool>(new ValidationException("Token is required."));
        }

        if (ValidationHelpers.HasEmptyOrWhiteSpace(request.Password, request.ConfirmPassword))
        {
            return new Result<bool>(
                new ValidationException("Password and password confirmation are required.")
            );
        }

        if (request.Password != request.ConfirmPassword)
        {
            return new Result<bool>(new ValidationException("Passwords do not match."));
        }

        if (request.Password.Length < ValidationHelpers.PasswordLengthMinimum)
        {
            return new Result<bool>(
                new ValidationException(
                    $"Password must be at least {ValidationHelpers.PasswordLengthMinimum} characters long."
                )
            );
        }

        var tokenHash = _securityService.HashToken(request.Token);
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            var passwordResetToken = await _context.PasswordResetTokens.FirstOrDefaultAsync(
                token =>
                    token.TokenHash == tokenHash
                    && token.UsedAt == null
                    && !token.IsRevoked
                    && token.ExpiresAt > currentTimestamp,
                cancellationToken
            );

            if (passwordResetToken is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<bool>(
                    new ValidationException("The password reset token is invalid or has expired.")
                );
            }

            var foundUser = await _context.Users.FirstOrDefaultAsync(
                user => user.Id == passwordResetToken.UserId,
                cancellationToken
            );

            if (foundUser is null)
            {
                await transaction.RollbackAsync(cancellationToken);

                return new Result<bool>(
                    new ValidationException("The password reset token is invalid or has expired.")
                );
            }

            foundUser.Password = _passwordService.HashPassword(request.Password);

            passwordResetToken.UsedAt = currentTimestamp;
            passwordResetToken.UpdatedDate = currentTimestamp;
            passwordResetToken.LastUpdatedBy = foundUser.Id;

            var otherActiveTokens = await _context
                .PasswordResetTokens.Where(token =>
                    token.UserId == foundUser.Id
                    && token.Id != passwordResetToken.Id
                    && token.UsedAt == null
                    && !token.IsRevoked
                )
                .ToListAsync(cancellationToken);

            foreach (var token in otherActiveTokens)
            {
                token.IsRevoked = true;
                token.UpdatedDate = currentTimestamp;
                token.LastUpdatedBy = foundUser.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new Result<bool>(true);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<bool>(
                new ValidationException(
                    "The password reset token is invalid or has already been used."
                )
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            return new Result<bool>(ex);
        }
    }

    public async Task<Result<bool>> UploadImage(CancellationToken cancellationToken)
    {
        // Check if file exist in request
        // Create image URL
        // Return 200
        throw new NotImplementedException();
    }
}
