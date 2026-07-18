using Expensify.API.DTOs.ExpenseDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses
{
    public class ExpenseService(ApplicationDbContext _context) : IExpenseService
    {
        public Task<Result<ExpenseTransactionResponseDTO>> AddExpense(
            Guid userId,
            AddExpenseTransactionDTO request,
            CancellationToken cancellationToken
        )
        {
            // Validate all data is present in request
            // If not throw 400 .. All fields are required

            // Create income data return 200

            // Catch error return 500
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> DeleteExpense(Guid id, CancellationToken cancellationToken)
        {
            // Find and delete income by id
            // Return message to indicate success

            // Catch error return 500
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> DownloadExpenseExcel(
            DownloadExpenseExcelDTO request,
            CancellationToken cancellationToken
        )
        {
            // Find User Income based on id
            // Prepare Data for Excel

            // Catch error return 500
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> GetAllExpense(
            GetExpenseIncomeDTO request,
            CancellationToken cancellationToken
        )
        {
            // Grab user id

            // Try to find the income based on the user id and sort by date
            // Return data

            // Catch error return 500
            throw new NotImplementedException();
        }
    }
}
