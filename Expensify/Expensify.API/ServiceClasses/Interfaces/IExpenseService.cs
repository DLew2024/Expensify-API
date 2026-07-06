using Expensify.DTOs.ExpenseDTOs;

namespace Expensify.Services.Interfaces
{
    // Use DTOS
    public interface IExpenseService
    {
        void AddExpense(AddExpenseDTO request, CancellationToken cancellationToken);
        void GetAllExpense(GetAllExpenseDTO request, CancellationToken cancellationToken);
        void DeleteExpense(Guid id, CancellationToken cancellationToken);
        void DownloadExpenseExcel(DownloadExpenseExcelDTO request, CancellationToken cancellationToken);
    }
}
