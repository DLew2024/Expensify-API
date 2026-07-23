using Expensify.API.DTOs.DashboardDTOs;
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
    Task<Result<List<TransactionDTO>>> GetAllExpense(
        Guid userId,
        Guid? accountId,
        CancellationToken cancellationToken
    );
    Task<Result<bool>> DeleteExpense(
        Guid userId,
        Guid expenseId,
        CancellationToken cancellationToken
    );
    Task<Result<byte[]>> DownloadExpenseExcel(Guid userId, CancellationToken cancellationToken);
}
