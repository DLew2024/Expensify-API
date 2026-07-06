using Expensify.API.DTOs.AuthDTOs;
using Expensify.DTOs.AuthDTOs;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserResponseDTO>> GetUserInfo(Guid id, CancellationToken cancellationToken);
        Task<Result<UserTokenResponseDTO>> LoginUser(
            LoginUserDTO request,
            CancellationToken cancellationToken
        );
        Task<Result<UserTokenResponseDTO>> RegisterUser(
            RegisterUserDTO request,
            CancellationToken cancellationToken
        );

        Task<Result<bool>> UploadImage(CancellationToken cancellationToken);
    }
}
