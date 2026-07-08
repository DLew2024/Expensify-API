using Expensify.API.DTOs.AuthDTOs;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses.Interfaces;

public interface IAuthService
{
    Task<Result<bool>> ForgotPassword(
        ForgotPasswordDTO request,
        CancellationToken cancellationToken
    );

    Task<Result<UserResponseDTO>> GetUserInfo(Guid id, CancellationToken cancellationToken);
    Task<Result<UserTokenResponseDTO>> LoginUser(
        LoginUserDTO request,
        CancellationToken cancellationToken
    );
    Task<Result<UserTokenResponseDTO>> RegisterUser(
        RegisterUserDTO request,
        CancellationToken cancellationToken
    );

    Task<Result<bool>> ResetPassword(ResetDTO request, CancellationToken cancellationToken);

    Task<Result<bool>> UploadImage(CancellationToken cancellationToken);
}
