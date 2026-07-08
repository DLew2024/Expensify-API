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
            //1.Receive email.
            if (!ValidationHelpers.IsValidEmail(request.Email))
            {
                return new Result<bool>(
                    new ValidationException("Passed email is not in correct format.")
                );
            }

            var normalizedEmail = ValidationHelpers.Normalize(request.Email);

            //2.Look up user by email.
            var foundUser = await _context.Users.FirstOrDefaultAsync(
                _ => _.Email.Equals(normalizedEmail, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken
            );

            //3.Always return success, even if user does not exist. To prevent user enumeration
            if (foundUser is null)
            {
                return new Result<bool>(true);
            }

            //4.Generate secure reset token.
            var existingTokens = await _context
                .PasswordResetTokens.Where(_ =>
                    _.UserId == foundUser.Id && _.UsedAt == null && !_.IsRevoked
                )
                .ToListAsync(cancellationToken);

            foreach (var existingToken in existingTokens)
            {
                existingToken.IsRevoked = true;
                existingToken.UpdatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                existingToken.LastUpdatedBy = foundUser.Id;
            }

            //5.Hash token before saving to database.
            var rawToken = _securityService.GenerateSecureToken();
            var tokenHash = _securityService.HashToken(rawToken);
            var nowCreatedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            //6.Save token hash, user id, expiration date, and used flag.
            var passwordResetToken = new PasswordResetToken
            {
                UserId = foundUser.Id,
                TokenHash = tokenHash,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds(),
                CreatedBy = foundUser.Id,
                CreateDate = nowCreatedDate,
            };

            _context.PasswordResetTokens.Add(passwordResetToken);

            await _context.SaveChangesAsync(cancellationToken);

            //7.Email user a reset link with the raw token.
            var resetLink =
                $"{_frontendSettings.BaseUrl.TrimEnd('/')}/reset-password?token={Uri.EscapeDataString(rawToken)}";

            await _emailService.SendPasswordResetEmail(
                foundUser.Email,
                resetLink,
                cancellationToken
            );

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

                if (userInfo == null)
                {
                    return new Result<UserResponseDTO>(
                        new EntityNotFoundException("User with id was not found" + id)
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

            try
            {
                var foundUser = await _context
                    .Users.AsNoTracking()
                    .Where(user => user.Email == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (foundUser == null)
                {
                    return new Result<UserTokenResponseDTO>(
                        new EntityNotFoundException(
                            "No user could be found with the email:" + request.Email
                        )
                    );
                }
                else if (!_passwordService.VerifyPassword(request.Password, foundUser.Password))
                {
                    return new Result<UserTokenResponseDTO>(
                        new UnauthorizedAccessException("Incorrect Password")
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

            try
            {
                var userExists = await _context.Users.AnyAsync(
                    user => user.Email == request.Email,
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
                    Email = request.Email,
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
