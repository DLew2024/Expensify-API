using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses.Resolvers;

public class AccountResolver : IAccountResolver
{
    private readonly ApplicationDbContext _context;

    public AccountResolver(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> ResolveAccountId(
        Guid userId,
        Guid? accountId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .Accounts.AsNoTracking()
            .Where(account =>
                account.UserId == userId
                && !account.IsDeleted
                && account.IsActive
                && (accountId.HasValue ? account.Id == accountId.Value : account.IsDefault)
            )
            .Select(account => (Guid?)account.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
