using Expensify.API.DTOs.ExpenseDTOs;

namespace Expensify.API.ServiceClasses.Interfaces
{
    // Use DTOS
    public interface IExpenseService
    {
        void AddExpense(AddExpenseDTO request, CancellationToken cancellationToken);
        void GetAllExpense(GetAllExpenseDTO request, CancellationToken cancellationToken);
        void DeleteExpense(Guid id, CancellationToken cancellationToken);
        void DownloadExpenseExcel(
            DownloadExpenseExcelDTO request,
            CancellationToken cancellationToken
        );
    }
}
