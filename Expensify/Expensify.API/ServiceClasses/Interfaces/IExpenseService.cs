using Expensify.API.DTOs.ExpenseDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses.Interfaces;

public interface IExpenseService
{
    Task<Result<ExpenseTransactionResponseDTO>> AddExpense(
        Guid userId,
        AddExpenseTransactionDTO request,
        CancellationToken cancellationToken
    );
    Task<Result<bool>> GetAllExpense(
        GetExpenseIncomeDTO request,
        CancellationToken cancellationToken
    );
    Task<Result<bool>> DeleteExpense(Guid id, CancellationToken cancellationToken);
    Task<Result<bool>> DownloadExpenseExcel(
        DownloadExpenseExcelDTO request,
        CancellationToken cancellationToken
    );
}
