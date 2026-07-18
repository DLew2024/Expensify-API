using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Enums;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Services.Interfaces
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DashboardDataResponseDTO>> GetDashboardData(
            Guid userId,
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

                var currentDate = DateTimeOffset.UtcNow;

                var thirtyDaysAgo = currentDate.AddDays(-30).ToUnixTimeSeconds();
                var sixtyDaysAgo = currentDate.AddDays(-60).ToUnixTimeSeconds();

                var transactions = await _context
                    .Transactions.AsNoTracking()
                    .Where(transaction => transaction.UserId == userId)
                    .Select(TransactionDTO.Projection)
                    .ToListAsync(cancellationToken);

                var totalIncome = transactions
                    .Where(transaction => transaction.Type == TransactionType.Income)
                    .Sum(transaction => transaction.Amount);

                var totalExpenses = transactions
                    .Where(transaction => transaction.Type == TransactionType.Expense)
                    .Sum(transaction => transaction.Amount);

                var totalBalance = totalIncome - totalExpenses;

                var last30DaysOfExpenses = transactions
                    .Where(transaction =>
                        transaction.Type == TransactionType.Expense
                        && transaction.TransactionDate >= thirtyDaysAgo
                    )
                    .OrderByDescending(transaction => transaction.TransactionDate)
                    .ToArray();

                var last60DaysOfExpenses = transactions
                    .Where(transaction =>
                        transaction.Type == TransactionType.Expense
                        && transaction.TransactionDate >= sixtyDaysAgo
                    )
                    .OrderByDescending(transaction => transaction.TransactionDate)
                    .ToArray();

                var last30DaysOfIncome = transactions
                    .Where(transaction =>
                        transaction.Type == TransactionType.Income
                        && transaction.TransactionDate >= thirtyDaysAgo
                    )
                    .OrderByDescending(transaction => transaction.TransactionDate)
                    .ToArray();

                var last60DaysOfIncome = transactions
                    .Where(transaction =>
                        transaction.Type == TransactionType.Income
                        && transaction.TransactionDate >= sixtyDaysAgo
                    )
                    .OrderByDescending(transaction => transaction.TransactionDate)
                    .ToArray();

                var recentTransactions = transactions
                    .OrderByDescending(transaction => transaction.TransactionDate)
                    .Take(5)
                    .ToArray();

                var response = new DashboardDataResponseDTO
                {
                    TotalBalance = totalBalance,
                    TotalIncome = totalIncome,
                    TotalExpenses = totalExpenses,

                    Last30DaysOfExpenses = new TransactionPeriodSummaryDTO
                    {
                        TotalBalance = last30DaysOfExpenses.Sum(transaction => transaction.Amount),
                        Transactions = last30DaysOfExpenses,
                    },

                    Last60DaysOfExpenses = new TransactionPeriodSummaryDTO
                    {
                        TotalBalance = last60DaysOfExpenses.Sum(transaction => transaction.Amount),
                        Transactions = last60DaysOfExpenses,
                    },

                    Last30DaysOfIncome = new TransactionPeriodSummaryDTO
                    {
                        TotalBalance = last30DaysOfIncome.Sum(transaction => transaction.Amount),
                        Transactions = last30DaysOfIncome,
                    },

                    Last60DaysOfIncome = new TransactionPeriodSummaryDTO
                    {
                        TotalBalance = last60DaysOfIncome.Sum(transaction => transaction.Amount),
                        Transactions = last60DaysOfIncome,
                    },

                    RecentTransactions = recentTransactions,
                };

                return new Result<DashboardDataResponseDTO>(response);
            }
            catch (Exception exception)
            {
                return new Result<DashboardDataResponseDTO>(exception);
            }
        }
    }
}
