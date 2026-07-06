using Expensify.DTOs.AuthDTOs;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Services.Interfaces
{
    // Use DTOS
    public interface IAuthService
    {
        Task<ActionResult> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken
        );
        Task<Result<bool>> LoginUser(LoginUserDTO request, CancellationToken cancellationToken);
        Task<ActionResult> GetUserInfo(CancellationToken cancellationToken);
        Task<ActionResult> UploadImage(CancellationToken cancellationToken);
    }
}
