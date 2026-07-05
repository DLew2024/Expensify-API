using Expensify.DTOs;
using Expensify.DTOs.AuthDTOs;
using Expensify.Models;
using Microsoft.AspNetCore.Mvc;
namespace Expensify.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ActionResult> RegisterUser(RegisterUserDTO request, CancellationToken cancellationToken);
        Task<ActionResult> LoginUser(LoginUserDto request, CancellationToken cancellationToken);
        Task<ActionResult> GetUserInfo(CancellationToken cancellationToken);
        Task<ActionResult> UploadImage(CancellationToken cancellationToken);
    }
}
