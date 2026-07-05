using Expensify.DTOs.AuthDTOs;
using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost(Name = "Register")]
        public async Task<ActionResult> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken)
        {
            var result = await service.RegisterUser(request, cancellationToken);

            return Ok(result);
        }

        [HttpPost(Name = "Login")]
        public void LoginUser(
            LoginUserDto request,
            CancellationToken cancellationToken
        )
        {
            
        }

        [HttpGet(Name = "GetUser")]
        public void GetUserInfo(CancellationToken cancellationToken)
        {
            
        }

        [HttpPost(Name = "Upload-Image")]
        public void UploadImage(CancellationToken cancellationToken)
        {
            
        }
    }
}
