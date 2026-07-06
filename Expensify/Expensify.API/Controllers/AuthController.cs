using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServicesClasses.Interfaces;
using Expensify.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IService _service;

        public AuthController(IService service)
        {
            _service = service;
        }

        [HttpPost(Name = "Register")]
        public async Task<ActionResult> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.AuthService.RegisterUser(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost(Name = "Login")]
        public async Task<ActionResult<LoginUserResponseDTO>> LoginUser(
            LoginUserDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.AuthService.LoginUser(request, cancellationToken);

            return result.Match<ActionResult<LoginUserResponseDTO>>(
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
        public void GetUserInfo(CancellationToken cancellationToken) { }

        [HttpPost(Name = "Upload-Image")]
        public void UploadImage(CancellationToken cancellationToken) { }
    }
}
