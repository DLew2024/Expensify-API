using Expensify.API.DTOs.AccountDTOs;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses.Interfaces;

public interface IAccountService
{
    Task<Result<CreateAccountResponseDTO>> CreateAccount(
        Guid userId,
        CreateAccountDTO request,
        CancellationToken cancellationToken
    );
}
