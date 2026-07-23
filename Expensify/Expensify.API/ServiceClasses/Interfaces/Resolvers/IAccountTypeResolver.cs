using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.API.ServiceClasses.Interfaces.Resolvers;

public interface IAccountTypeResolver
{
    Task<AccountType?> ResolveAccountTypeById(
        Guid userId,
        Guid accountTypeId,
        CancellationToken cancellationToken
    );
}
