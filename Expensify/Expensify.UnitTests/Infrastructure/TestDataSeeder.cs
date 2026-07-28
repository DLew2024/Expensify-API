using System.Xml.Linq;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.DataAccessLayer.Enums;
using Expensify.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.Infrastructure;

public sealed class TestDataSeeder(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Role> GetOrCreateUserRoleAsync(string name = "User")
    {
        var existingRole = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "User");

        if (existingRole is not null)
        {
            return existingRole;
        }

        var role = new Role { Id = Guid.NewGuid(), Name = name };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return role;
    }

    public async Task<CurrencyCode> GetOrCreateCurrencyCodeAsync(
        string code = "USD",
        string name = "US Dollar",
        string symbol = "$",
        bool isActive = true,
        bool isDeleted = false
    )
    {
        var existingCurrency = await _context.CurrencyCodes.FirstOrDefaultAsync(currency =>
            currency.Code == "USD"
        );

        if (existingCurrency is not null)
        {
            return existingCurrency;
        }

        var currency = new CurrencyCode
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Symbol = symbol,
            IsActive = isActive,
            IsDeleted = isDeleted,
        };

        _context.CurrencyCodes.Add(currency);
        await _context.SaveChangesAsync();

        return currency;
    }

    public async Task<AccountType> GetOrCreateAccountTypeAsync(
        Guid userId,
        string name = "Checking",
        bool isSystemDefault = true,
        bool isDeleted = false,
        bool IsActive = true
    )
    {
        var existingAccountType = await _context.AccountTypes.FirstOrDefaultAsync(accountType =>
            accountType.Name == "Checking" && accountType.IsSystemDefault && !accountType.IsDeleted
        );

        if (existingAccountType is not null)
        {
            return existingAccountType;
        }

        var accountType = new AccountType
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsActive = IsActive,
            IsDeleted = isDeleted,
            IsSystemDefault = isSystemDefault,
            CreatedBy = userId,
            LastUpdatedBy = userId,
        };

        _context.AccountTypes.Add(accountType);
        await _context.SaveChangesAsync();

        return accountType;
    }

    public async Task<User> GetOrCreateUserAsync(Guid? userId = null)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

        if (existingUser is not null)
        {
            return existingUser;
        }

        var role = await GetOrCreateUserRoleAsync();

        var user = new UserBuilder().WithId(userId ?? Guid.NewGuid()).WithRole(role).Build();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<Account> GetOrCreateAccountAsync(
        Guid? accountId = null,
        Guid? userId = null,
        bool isActive = true,
        bool isDeleted = false,
        bool isDefault = false
    )
    {
        var existingAccount = await _context.Accounts.FirstOrDefaultAsync(account =>
            account.Id == accountId && account.UserId == userId
        );

        if (existingAccount is not null)
        {
            return existingAccount;
        }

        var user = await GetOrCreateUserAsync(userId);
        var currency = await GetOrCreateCurrencyCodeAsync();
        var accountType = await GetOrCreateAccountTypeAsync(user.Id);

        var account = new AccountBuilder()
            .WithId(accountId ?? Guid.NewGuid())
            .WithUser(user)
            .WithCurrency(currency)
            .WithAccountType(accountType)
            .WithActiveStatus(isActive)
            .WithDeletedStatus(isDeleted)
            .WithDefaultStatus(isDefault)
            .Build();

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Transaction> CreateTransactionAsync(
        TransactionType transactionType,
        Guid? userId = null,
        Guid? transactionId = null,
        Guid? accountId = null,
        decimal amount = 100m,
        bool isDeleted = false
    )
    {
        var user = await GetOrCreateUserAsync(userId);
        var account = await GetOrCreateAccountAsync(accountId: accountId, userId: user.Id);

        var transaction = new TransactionBuilder()
            .WithId(transactionId ?? Guid.NewGuid())
            .WithUser(user)
            .WithAccount(account)
            .WithAmount(amount)
            .WithDeletedStatus(isDeleted)
            .Build(transactionType);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}
