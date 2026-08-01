using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.Services.Resolvers;

public class TransactionResolver(ApplicationDbContext context) : ITransactionResolver
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Resolves a specific non-deleted transaction belonging to the specified user
    /// and matching the requested transaction type.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the transaction.</param>
    /// <param name="transactionId">The unique identifier of the transaction to resolve.</param>
    /// <param name="transactionType">
    /// The required transaction type, such as income or expense.
    /// </param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The matching transaction if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Transaction?> ResolveTransactionByUserIdAndType(
        Guid userId,
        Guid transactionId,
        TransactionType transactionType,
        CancellationToken cancellationToken
    )
    {
        return await _context.Transactions.FirstOrDefaultAsync(
            transaction =>
                transaction.Id == transactionId
                && transaction.UserId == userId
                && transaction.Type == transactionType
                && !transaction.IsDeleted,
            cancellationToken
        );
    }

    /// <summary>
    /// Resolves all non-deleted transactions of the specified type for a user's account.
    /// Transactions are returned in descending order by transaction date, with the most
    /// recent transactions first.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the transactions.</param>
    /// <param name="accountId">The unique identifier of the account associated with the transactions.</param>
    /// <param name="transactionType">The type of transactions to retrieve, such as income or expense.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A list of transactions belonging to the specified user and account that match
    /// the requested transaction type.
    /// </returns>
    public async Task<
        List<TransactionDTO>
    > ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
        Guid userId,
        Guid accountId,
        TransactionType transactionType,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Transactions.AsNoTracking()
            .Where(transaction =>
                transaction.UserId == userId
                && transaction.AccountId == accountId
                && transaction.Type == transactionType
                && !transaction.IsDeleted
            )
            .OrderByDescending(transaction => transaction.TransactionDate)
            .Select(TransactionDTO.Projection)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Resolves all non-deleted transactions for a specific account belonging to the specified user.
    /// Transactions are projected directly to <see cref="TransactionDTO"/> objects.
    /// </summary>
    /// <param name="userId">The unique identifier of the user who owns the transactions.</param>
    /// <param name="accountId">The unique identifier of the account associated with the transactions.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An array of transactions associated with the specified user and account.
    /// </returns>
    public async Task<TransactionDTO[]> ResolveTransactionsByUserAndAccount(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Transactions.AsNoTracking()
            .Where(transaction =>
                transaction.UserId == userId
                && transaction.AccountId == accountId
                && !transaction.IsDeleted
            )
            .Select(TransactionDTO.Projection)
            .ToArrayAsync(cancellationToken);
    }
}
