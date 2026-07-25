using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses.Resolvers;

public class AccountResolver(ApplicationDbContext context) : IAccountResolver
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Resolves the unique identifier of an active account belonging to the specified user.
    /// If an account ID is provided, validates and returns that specific account ID.
    /// Otherwise, returns the user's default account ID.
    /// Soft-deleted and inactive accounts are excluded.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the account.</param>
    /// <param name="accountId">
    /// The optional unique identifier of a specific account.
    /// If null, the user's default account ID is resolved.
    /// </param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The matching account ID if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Guid?> ResolveAccountIdByUserId(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Accounts.AsNoTracking()
            .Where(account =>
                account.Id == accountId
                && account.UserId == userId
                && !account.IsDeleted
                && account.IsActive
            )
            .Select(account => (Guid?)account.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Resolves an active account belonging to the specified user.
    /// If an account ID is provided, returns that specific account.
    /// Otherwise, returns the user's default account.
    /// Soft-deleted and inactive accounts are excluded.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the account.</param>
    /// <param name="accountId">
    /// The optional unique identifier of a specific account.
    /// If null, the user's default account is resolved.
    /// </param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The matching active account if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Account?> ResolveAccountByUserId(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Accounts.AsNoTracking()
            .FirstOrDefaultAsync(
                account =>
                    account.Id == accountId
                    && account.UserId == userId
                    && account.IsActive
                    && !account.IsDeleted,
                cancellationToken
            );
    }

    /// <summary>
    /// Resolves the active account associated with a specific transaction.
    /// Ensures both the transaction and account belong to the specified user
    /// and have not been soft deleted.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the transaction and account.</param>
    /// <param name="transactionId">The unique identifier of the transaction used to resolve the associated account.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The account associated with the transaction if found and accessible; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Account?> ResolveAccountByTransactionId(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Transactions.Where(transaction =>
                transaction.Id == transactionId
                && transaction.UserId == userId
                && !transaction.IsDeleted
            )
            .Select(transaction => transaction.Account)
            .FirstOrDefaultAsync(
                account => account.UserId == userId && !account.IsDeleted,
                cancellationToken
            );
    }
}
