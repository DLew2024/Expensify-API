using Expensify.API.DTOs.AccountDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class AccountService(ApplicationDbContext context) : IAccountService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CreateAccountResponseDTO>> CreateAccount(
        Guid userId,
        CreateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var accountType = await _context
                .AccountTypes.AsNoTracking()
                .FirstOrDefaultAsync(
                    accountType =>
                        accountType.Id == request.AccountTypeId
                        && accountType.IsActive
                        && !accountType.IsDeleted
                        && (accountType.IsSystemDefault || accountType.UserId == userId),
                    cancellationToken
                );

            if (accountType == null)
            {
                return new Result<CreateAccountResponseDTO>(
                    new EntityNotFoundException(
                        "The selected account type could not be found or is unavailable."
                    )
                );
            }

            var currentDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var account = request.ToEntity(userId);

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync(cancellationToken);

            return CreateAccountResponseDTO.FromEntity(account, accountType.Name);
        }
        catch (Exception ex)
        {
            return new Result<CreateAccountResponseDTO>(ex);
        }
    }

    public async Task<Result<bool>> DeleteAccount(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        var closedDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var affectedRows = await _context
            .Accounts.Where(account =>
                account.Id == accountId && account.UserId == userId && !account.IsDeleted
            )
            .ExecuteUpdateAsync(
                setters =>
                    setters
                        .SetProperty(account => account.IsDeleted, true)
                        .SetProperty(account => account.IsActive, false)
                        .SetProperty(account => account.ClosedDate, closedDate),
                cancellationToken
            );

        if (affectedRows == 0)
        {
            return new Result<bool>(
                new EntityNotFoundException($"Account with id '{accountId}' was not found.")
            );
        }

        return new Result<bool>(true);
    }

    public async Task<Result<IEnumerable<AccountResponseDTO>>> GetAccounts(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var accounts = await _context
                .Accounts.AsNoTracking()
                .Where(account => account.UserId == userId && !account.IsDeleted)
                .Select(AccountResponseDTO.Projection)
                .ToListAsync(cancellationToken);

            return accounts;
        }
        catch (Exception ex)
        {
            return new Result<IEnumerable<AccountResponseDTO>>(ex);
        }
    }
}
