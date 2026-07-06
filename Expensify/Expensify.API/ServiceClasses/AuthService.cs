using System.ComponentModel.DataAnnotations;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DTOs.AuthDTOs;
using Expensify.Services.Interfaces;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            //try
            //{
            //    // Get user by ID and select everything but password
            //    // Validate user exists
            //    return;

            //}
            //catch
            //{

            //}

            throw new NotImplementedException();
        }

        public async Task<Result<bool>> LoginUser(
            LoginUserDTO request,
            CancellationToken cancellationToken
        )
        {
            // Check for email and passowrd
            // If no email exists for that user than return 400
            if (
                string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.Password)
            )
            {
                throw new ValidationException("Email and password are required.");
            }

            try
            {
                // Find user by email
                var foundUser = await _context.Users.FirstOrDefaultAsync(
                    _ => _.Email == request.Email,
                    cancellationToken
                );

                // If user not found
                // Return 400
                if (foundUser == null)
                {
                    return new Result<bool>(
                        new EntityNotFoundException(
                            "No user could be found with the email:" + request.Email
                        )
                    );
                }

                // Passed password does not match the stored passcode
                // Return 400

                // Else return the user the id and the token

                //return Ok();
                return new Result<bool>(true);
            }
            catch (Exception e)
            {
                return new Result<bool>(new Exception(e.Message));
            }
        }

        public Task<ActionResult> RegisterUser(
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

        public Task<ActionResult> UploadImage(CancellationToken cancellationToken)
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
