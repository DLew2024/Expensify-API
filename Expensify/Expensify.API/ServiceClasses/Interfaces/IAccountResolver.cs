namespace Expensify.API.ServiceClasses.Interfaces;

public interface IAccountResolver
{
    Task<Guid?> ResolveAccountId(Guid userId, Guid? accountId, CancellationToken cancellationToken);
}
