using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.API.ServiceClasses.Interfaces.Resolvers;

public interface IAccountResolver
{
    Task<Guid?> ResolveAccountIdByUserId(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    );
    Task<Account?> ResolveAccountByUserId(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    );
    Task<Account?> ResolveAccountByTransactionId(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken
    );
}
