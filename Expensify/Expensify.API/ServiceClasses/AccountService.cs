using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AccountDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class AccountService(ApplicationDbContext context, IAccountTypeResolver accountTypeResolver)
    : IAccountService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAccountTypeResolver _accountTypeResolver = accountTypeResolver;

    public async Task<Result<AccountResponseDTO>> CreateAccount(
        Guid userId,
        CreateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (request.AccountTypeId is null)
            {
                return new Result<AccountResponseDTO>(
                    new ValidationException("The AccountTypeId could not null.")
                );
            }

            var accountType = await _accountTypeResolver.ResolveAccountTypeById(
                userId,
                request.AccountTypeId.Value,
                cancellationToken
            );

            if (accountType == null)
            {
                return new Result<AccountResponseDTO>(
                    new EntityNotFoundException(
                        "The selected account type could not be found or is unavailable."
                    )
                );
            }

            var hasExistingAccounts = await _context.Accounts.AnyAsync(
                account => account.UserId == userId && !account.IsDeleted,
                cancellationToken
            );

            var account = request.ToEntity(userId);

            account.IsDefault = !hasExistingAccounts;

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync(cancellationToken);

            return AccountResponseDTO.FromEntity(account, accountType.Name);
        }
        catch (Exception ex)
        {
            return new Result<AccountResponseDTO>(ex);
        }
    }

    public async Task<Result<bool>> SetDefaultAccount(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var accounts = await _context
                .Accounts.Where(account => account.UserId == userId && !account.IsDeleted)
                .ToListAsync(cancellationToken);

            var newDefaultAccount = accounts.FirstOrDefault(account => account.Id == accountId);

            if (newDefaultAccount is null)
            {
                return new Result<bool>(
                    new EntityNotFoundException("The account could not be found.")
                );
            }

            foreach (var account in accounts)
            {
                account.IsDefault = account.Id == accountId;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<bool>(true);
        }
        catch (Exception ex)
        {
            return new Result<bool>(ex);
        }
    }

    // Make sure user cant delete account if only one is left
    public async Task<Result<bool>> DeleteAccount(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        var account = await _context
            .Accounts.AsNoTracking()
            .FirstOrDefaultAsync(
                account =>
                    account.Id == accountId && account.UserId == userId && !account.IsDeleted,
                cancellationToken
            );

        if (account is null)
        {
            return new Result<bool>(
                new EntityNotFoundException($"Account with id '{accountId}' was not found.")
            );
        }

        if (account.IsDefault)
        {
            return new Result<bool>(
                new ValidationException(
                    "The default account cannot be deleted. Set another account as the default first."
                )
            );
        }

        var accountCount = await _context.Accounts.CountAsync(
            account => account.UserId == userId && !account.IsDeleted && account.IsActive,
            cancellationToken
        );

        if (accountCount <= 1)
        {
            return new Result<bool>(
                new ValidationException("You must have at least one active account.")
            );
        }

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

        return new Result<bool>(affectedRows > 0);
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
