using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.API.Services.Interfaces.Resolvers;

public interface IAccountTypeResolver
{
    Task<AccountType?> ResolveAccountTypeById(
        Guid userId,
        Guid accountTypeId,
        CancellationToken cancellationToken
    );
}
