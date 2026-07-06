using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Functions;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DTOs.AuthDTOs;
using Expensify.Entities.Models;
using Expensify.Services.Interfaces;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<Result<UserResponseDTO>> GetUserInfo(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(
                    user => user.Id == id,
                    cancellationToken
                );

                if (foundUser == null)
                {
                    return new Result<UserResponseDTO>(
                        new EntityNotFoundException("User with id was not found" + id)
                    );
                }

                var userInfo = new UserResponseDTO
                {
                    FullName = foundUser.FullName,
                    Email = foundUser.Email,
                    ProfileImageURl = foundUser.ProfileImageURl,
                };

                return new Result<UserResponseDTO>(userInfo);
            }
            catch (Exception ex)
            {
                return new Result<UserResponseDTO>(ex);
            }
        }

        public async Task<Result<LoginUserResponseDTO>> LoginUser(
            LoginUserDTO request,
            CancellationToken cancellationToken
        )
        {
            if (
                string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.Password)
            )
            {
                return new Result<LoginUserResponseDTO>(
                    new ValidationException("Email and password are required.")
                );
            }

            try
            {
                User? foundUser = await _context.Users.FirstOrDefaultAsync(
                    user => user.Email == request.Email,
                    cancellationToken
                );

                if (foundUser == null)
                {
                    return new Result<LoginUserResponseDTO>(
                        new EntityNotFoundException(
                            "No user could be found with the email:" + request.Email
                        )
                    );
                }
                else if (!PasswordValidation.ValidatePassword(request.Password, foundUser.Password))
                {
                    return new Result<LoginUserResponseDTO>(
                        new UnauthorizedAccessException("Invalid email or password.")
                    );
                }

                var token = _jwtService.GenerateToken(foundUser);

                return new Result<LoginUserResponseDTO>(
                    new LoginUserResponseDTO { UserId = foundUser.Id, Token = token }
                );
            }
            catch (Exception ex)
            {
                return new Result<LoginUserResponseDTO>(ex);
            }
        }

        public Task<Result<bool>> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken
        )
        {
            // Validation check for missing fields
            // Return a bad request response if any field is missing

            //try
            //{
            // Try to find a a;ready existing user based on email
            // If one exists return the 409 code

            // If that's not the case go ahead and create a new user
            // Resposne 201 with token generated and added

            //    return Ok(200);

            //}
            //catch
            //{
            //    // Catch any error that occurs in the try block
            //}
            throw new NotImplementedException();
        }

        public Task<Result<bool>> UploadImage(CancellationToken cancellationToken)
        {
            // Check if file exist in request
            // Create image URL
            // Return 200
            throw new NotImplementedException();
        }

        //const generateToken = (string userId, string secretKey) =>
        //{
        //    // Implement token generation logic here
        //    return "generated_token";
        //};

        //const isValidUser = (User user) =>
        //{
        //    return;
        //};
    }
}
