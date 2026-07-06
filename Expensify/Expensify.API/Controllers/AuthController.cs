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
            CancellationToken cancellationToken)
        {
            var result = await _service.AuthService.RegisterUser(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost(Name = "Login")]
        public void LoginUser(
            LoginUserDTO request,
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
