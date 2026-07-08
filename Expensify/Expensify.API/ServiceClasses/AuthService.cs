using System.ComponentModel.DataAnnotations;
using Expensify.API.Configurations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Functions;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Expensify.API.ServiceClasses
{
    public class AuthService(
        ApplicationDbContext context,
        IJwtService jwtService,
        IPasswordService passwordService,
        ISecurityService securityService,
        IEmailService emailService,
        IOptions<FrontendSettings> frontendOptions
    ) : IAuthService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ISecurityService _securityService = securityService;
        private readonly IEmailService _emailService = emailService;
        private readonly FrontendSettings _frontendSettings = frontendOptions.Value;

        public async Task<Result<bool>> ForgotPassword(
            ForgotPasswordDTO request,
            CancellationToken cancellationToken
        )
        {
            if (!ValidationHelpers.IsValidEmail(request.Email))
            {
                return new Result<bool>(
                    new ValidationException("Passed email is not in correct format.")
                );
            }

            var normalizedEmail = ValidationHelpers.Normalize(request.Email);

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
                    .Select(user => new UserResponseDTO
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        ProfileImageURl = user.ProfileImageUrl,
                    })
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
            if (ValidationHelpers.HasEmptyOrWhiteSpace(request.Email, request.Password))
            {
                return new Result<UserTokenResponseDTO>(
                    new ValidationException("Email and password are required.")
                );
            }

            if (!ValidationHelpers.IsValidEmail(request.Email))
            {
                return new Result<UserTokenResponseDTO>(
                    new ValidationException("Passed email is not in correct format.")
                );
            }

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
                    new UserTokenResponseDTO { UserId = foundUser.Id, Token = token }
                );
            }
            catch (Exception ex)
            {
                return new Result<UserTokenResponseDTO>(ex);
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

            if (!ValidationHelpers.IsValidEmail(request.Email))
            {
                return new Result<UserTokenResponseDTO>(
                    new ValidationException("Passed email is not in correct format.")
                );
            }

            var normalizedEmail = ValidationHelpers.Normalize(request.Email);

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
                    FullName = $"{request.FirstName} {request.LastName}",
                    Email = normalizedEmail,
                    Password = _passwordService.HashPassword(request.Password),
                    ProfileImageUrl = request.ProfileImageURl,
                };

                _context.Users.Add(newUser);

                await _context.SaveChangesAsync(cancellationToken);

                var token = _jwtService.GenerateToken(newUser);

                return new Result<UserTokenResponseDTO>(
                    new UserTokenResponseDTO { UserId = newUser.Id, Token = token }
                );
            }
            catch (DbUpdateException ex)
            {
                // Handles race conditions where another request inserts the same email
                return new Result<UserTokenResponseDTO>(
                    new ConflictException("A user with this email already exists.", ex)
                );
            }
            catch (Exception ex)
            {
                return new Result<UserTokenResponseDTO>(ex);
            }
        }

        public Task<Result<bool>> ResetPassword(
            ResetPasswordDTO request,
            CancellationToken cancellationToken
        )
        {
            // 1. Validate request

            // 2. Hash raw token

            // 3. Find valid password reset token

            // 4. Return error if invalid

            // 5. Find user

            // 6. Update password

            // 7. Mark token as used

            // 8. Revoke other reset tokens

            // 9. Save changes

            // 10. Return true
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> UploadImage(CancellationToken cancellationToken)
        {
            // Check if file exist in request
            // Create image URL
            // Return 200
            throw new NotImplementedException();
        }
    }
}
