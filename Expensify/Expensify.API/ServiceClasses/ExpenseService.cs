using Expensify.DataAccessLayer;
using Expensify.DTOs.ExpenseDTOs;
using Expensify.Services.Interfaces;

namespace Expensify.Services
{
    public class ExpenseService(ApplicationDbContext _context) : IExpenseService
    {
        public void AddExpense(AddExpenseDTO request, CancellationToken cancellationToken)
        {
            // Validate all data is present in request
            // If not throw 400 .. All fields are required

            // Create income data return 200

            // Catch error return 500
            throw new NotImplementedException();
        }

        public void DeleteExpense(Guid id, CancellationToken cancellationToken)
        {
            // Find and delete income by id
            // Return message to indicate success

            // Catch error return 500
            throw new NotImplementedException();
        }

        public void DownloadExpenseExcel(
            DownloadExpenseExcelDTO request,
            CancellationToken cancellationToken
        )
        {
            // Find User Income based on id
            // Prepare Data for Excel

            // Catch error return 500
            throw new NotImplementedException();
        }

        public void GetAllExpense(GetAllExpenseDTO request, CancellationToken cancellationToken)
        {
            // Grab user id

            // Try to find the income based on the user id and sort by date
            // Return data

            // Catch error return 500
            throw new NotImplementedException();
        }
    }
}
