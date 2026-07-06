using System.ComponentModel.DataAnnotations;
using Expensify.API.Controllers;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServicesClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    public class AuthController : AuthorizationController
    {
        private readonly IService _service;

        public AuthController(IService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost(Name = "Register")]
        public async Task<ActionResult<UserTokenResponseDTO>> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.AuthService.RegisterUser(request, cancellationToken);
            return result.Match<ActionResult>(
                succees => StatusCode(StatusCodes.Status201Created),
                error =>
                    error switch
                    {
                        ValidationException ex => BadRequest(ex.Message),
                        ConflictException ex => Conflict(ex.Message),
                        _ => StatusCode(500, error.Message),
                    }
            );
        }

        // Add unauthroized logs 
        [AllowAnonymous]
        [HttpPost(Name = "Login")]
        public async Task<ActionResult<UserTokenResponseDTO>> LoginUser(
            LoginUserDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.AuthService.LoginUser(request, cancellationToken);

            return result.Match<ActionResult<UserTokenResponseDTO>>(
                succees => Ok(succees),
                error =>
                    error switch
                    {
                        UnauthorizedAccessException ex => Unauthorized(ex.Message),
                        ValidationException ex => BadRequest(ex.Message),
                        _ => StatusCode(500, error.Message),
                    }
            );
        }

        [HttpGet(Name = "GetUser")]
        public async Task<ActionResult<UserResponseDTO>> GetUserInfo(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.AuthService.GetUserInfo(id, cancellationToken);

            return result.Match<ActionResult<UserResponseDTO>>(
                success => Ok(),
                error =>
                    error switch
                    {
                        ValidationException ex => BadRequest(ex.Message),
                        _ => StatusCode(500, error.Message),
                    }
            );
        }

        [HttpPost(Name = "Upload-Image")]
        public void UploadImage(CancellationToken cancellationToken) { }
    }
}
