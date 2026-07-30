using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.Services.Resolvers;

public class AccountTypeResolver(ApplicationDbContext context) : IAccountTypeResolver
{
    private readonly ApplicationDbContext _context = context;

    public async Task<AccountType?> ResolveAccountTypeById(
        Guid userId,
        Guid accountTypeId,
        CancellationToken cancellationToken
    )
    {
        return await _context
            .AccountTypes.AsNoTracking()
            .FirstOrDefaultAsync(
                accountType =>
                    accountType.Id == accountTypeId
                    && accountType.IsActive
                    && !accountType.IsDeleted
                    && (accountType.IsSystemDefault || accountType.UserId == userId),
                cancellationToken
            );
    }
}
