using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Functions;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;

        public AuthService(
            ApplicationDbContext context,
            IJwtService jwtService,
            IPasswordService passwordService
        )
        {
            _context = context;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        public Task<Result<bool>> ForgotPassword(
            ForgotPasswordDTO request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
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
