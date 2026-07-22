using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Services.Interfaces
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountResolver _accountResolver;

        public DashboardService(ApplicationDbContext context, IAccountResolver accountResolver)
        {
            _context = context;
            _accountResolver = accountResolver;
        }

        public async Task<Result<DashboardDataResponseDTO>> GetDashboardData(
            Guid userId,
            Guid? accountId,
            CancellationToken cancellationToken
        )
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return new Result<DashboardDataResponseDTO>(
                        new UnauthorizedAccessException("Unable to identify the current user.")
                    );
                }

                var resolvedAccountId = await _accountResolver.ResolveAccountId(
                    userId,
                    accountId,
                    cancellationToken
                );

                if (!resolvedAccountId.HasValue)
                {
                    return new Result<DashboardDataResponseDTO>(
                        new EntityNotFoundException(
                            accountId.HasValue
                                ? "The selected account could not be found or is unavailable."
                                : "No default account could be found."
                        )
                    );
                }

                var account = await _context
                    .Accounts.AsNoTracking()
                    .Where(account =>
                        account.UserId == userId
                        && account.Id == resolvedAccountId.Value
                        && !account.IsDeleted
                        && account.IsActive
                    )
                    .Select(account => new AccountSummaryDTO
                    {
                        Id = account.Id,
                        Name = account.Name,
                        CurrentBalance = account.CurrentBalance,
                    })
                    .FirstAsync(cancellationToken);

                var transactions = await _context
                    .Transactions.AsNoTracking()
                    .Where(transaction =>
                        transaction.UserId == userId && transaction.AccountId == account.Id
                    )
                    .Select(TransactionDTO.Projection)
                    .ToArrayAsync(cancellationToken);

                var response = DashboardDataResponseDTO.Create(account, transactions);

                return new Result<DashboardDataResponseDTO>(response);
            }
            catch (Exception exception)
            {
                return new Result<DashboardDataResponseDTO>(exception);
            }
        }
    }
}
