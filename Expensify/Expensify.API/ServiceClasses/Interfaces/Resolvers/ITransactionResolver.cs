using Expensify.API.DTOs.DashboardDTOs;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.ServiceClasses.Interfaces.Resolvers;

public interface ITransactionResolver
{
    Task<Transaction?> ResolveTransactionByUserIdAndType(
        Guid userId,
        Guid transactionId,
        TransactionType transactionType,
        CancellationToken cancellationToken
    );
    Task<List<TransactionDTO>> ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
        Guid userId,
        Guid accountId,
        TransactionType transactionType,
        CancellationToken cancellationToken
    );
}
