using Expensify.DTOs.AuthDTOs;
using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Services
{
    public class AuthService : IAuthService
    {
        public Task<ActionResult> GetUserInfo(CancellationToken cancellationToken
)
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

        public Task<ActionResult> LoginUser(LoginUserDto request, CancellationToken cancellationToken
)
        {
            // Check for email and passowrd 
            // If no email exists for that user than return 400

            //try
            //{
                // Find user by email

                // If user not found or the passed password does not match the stored passcode 
                // Return 400

                // Else return the user the id and the token

            //    return;

            //}
            //catch
            //{

            //}
            throw new NotImplementedException();
        }

        public Task<ActionResult> RegisterUser(RegisterUserDTO request, CancellationToken cancellationToken
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

        public Task<ActionResult> UploadImage(CancellationToken cancellationToken
)
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
