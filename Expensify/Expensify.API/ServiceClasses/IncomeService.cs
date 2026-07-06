using Expensify.DataAccessLayer;
using Expensify.DTOs.IncomeDTOs;
using Expensify.Services.Interfaces;

namespace Expensify.Services
{
    public class IncomeService(ApplicationDbContext _context) : IIncomeService
    {
        public void AddIncome(AddIncomeDTO request, CancellationToken cancellationToken)
        {
            // Validate all data is present in request
            // If not throw 400 .. All fields are required

            // Create income data return 200

            // Catch error return 500 
            throw new NotImplementedException();
        }

        public void DeleteIncome(Guid id, CancellationToken cancellationToken)
        {
            // Find and delete income by id
            // Return message to indicate success 

            // Catch error return 500 
            throw new NotImplementedException();
        }
        public void DownloadIncomeExcel(DownloadIncomeExcelDTO request, CancellationToken cancellationToken)
        {
            // Find User Income based on id
            // Prepare Data for Excel 

            // Catch error return 500 
            throw new NotImplementedException();
        }

        public void GetAllIncome(GetAllIncomeDTO request, CancellationToken cancellationToken)
        {
            // Grab user id

            // Try to find the income based on the user id and sort by date
            // Return data

            // Catch error return 500 
            throw new NotImplementedException();
        }
    }
}
