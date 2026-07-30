using Expensify.API.DTOs.AccountDTOs;
using LanguageExt.Common;

namespace Expensify.API.Services.Interfaces;

public interface IAccountService
{
    Task<Result<AccountResponseDTO>> CreateAccount(
        Guid userId,
        CreateAccountDTO request,
        CancellationToken cancellationToken
    );
    Task<Result<bool>> DeleteAccount(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    );
    Task<Result<IEnumerable<AccountResponseDTO>>> GetAccounts(
        Guid userId,
        CancellationToken cancellationToken
    );
}
