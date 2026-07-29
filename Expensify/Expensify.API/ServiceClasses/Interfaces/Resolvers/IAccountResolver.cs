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
    Task<List<Account>> ResolveAccountsByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Account?> ResolveAccountByUserIdWithTracking(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    );
    Task<Account?> ResolveAccountByTransactionId(
        Guid userId,
        Guid transactionId,
        CancellationToken cancellationToken
    );
    Task<bool> HasExistingAccountsByUserId(Guid userId, CancellationToken cancellationToken);
}
