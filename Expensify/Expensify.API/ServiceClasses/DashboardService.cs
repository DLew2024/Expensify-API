using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using LanguageExt.Common;

namespace Expensify.Services.Interfaces
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Result<DashboardDataResponseDTO>> GetDashboardData(
            CancellationToken cancellationToken
        )
        {
            // Try to get user id

            // Fetch total Income and Expenses

            // Find Income by id add all Incomes by amount

            // Find Expenses by id add all Expenses by amount

            // Find last 60 days Income Transactions

            // Find income from last 60 days

            // Find last 30 days for Expenses Transactions

            // Find Expenses from last 30 days

            // Find last five transactions (income + expenses) sort the latest first

            // Final Response

            // Total Balance
            // Total Income
            // Total Expenses
            // Last 30 Days Expenses - {total, transactions}
            // Last 60 Days Expeses - {total, transactions}
            // recent

            // Catch errors
            throw new NotImplementedException();
        }
    }
}
