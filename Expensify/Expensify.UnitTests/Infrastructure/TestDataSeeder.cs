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

    public async Task<PaymentMethod> GetOrCreatePaymentMethodAsync(
        Guid? paymentMethodId,
        string name = "Test Payment Method",
        bool isDeleted = false
    )
    {
        var existingPaymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm =>
            pm.Name == "Test Payment Method"
        );

        if (existingPaymentMethod is not null)
        {
            return existingPaymentMethod;
        }

        var paymentMethod = new PaymentMethodBuilder()
            .WithId(paymentMethodId ?? Guid.NewGuid())
            .WithName(name)
            .WithDeletedStatus(isDeleted)
            .Build();

        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();

        return paymentMethod;
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

    public async Task<AccountType> GetOrCreateAccountTypeAsync(
        Guid? userId = null,
        Guid? accountTypeId = null,
        string name = "Test Checking Account",
        bool isActive = true,
        bool isDeleted = false,
        bool isSystemDefault = false
    )
    {
        var existingAccountType = await _context.AccountTypes.FirstOrDefaultAsync(accountType =>
            accountType.Id == accountTypeId
        );

        if (existingAccountType is not null)
        {
            return existingAccountType;
        }

        var user = await GetOrCreateUserAsync(userId);

        var accountType = new AccountTypeBuilder()
            .WithId(accountTypeId ?? Guid.NewGuid())
            .WithUser(user)
            .WithName(name)
            .WithSystemDefault(isSystemDefault)
            .WithDeletedStatus(isDeleted)
            .WithActiveStatus(isActive)
            .Build();

        _context.AccountTypes.Add(accountType);
        await _context.SaveChangesAsync();

        return accountType;
    }

    public async Task<Account> GetOrCreateAccountAsync(
        Guid? accountId = null,
        Guid? userId = null,
        bool isActive = true,
        bool isDeleted = false,
        bool isDefault = false,
        decimal initialBalance = 100m,
        long? closedDate = null
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
        var accountType = await GetOrCreateAccountTypeAsync(userId: user.Id);

        var account = new AccountBuilder()
            .WithId(accountId ?? Guid.NewGuid())
            .WithUser(user)
            .WithCurrency(currency)
            .WithAccountType(accountType)
            .WithActiveStatus(isActive)
            .WithDeletedStatus(isDeleted)
            .WithDefaultStatus(isDefault)
            .WithBalance(initialBalance)
            .Build();

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<CurrencyCode> GetOrCreateCurrencyCodeAsync(
        Guid? currencyCodeId = null,
        string code = "Test USD",
        string name = "US Dollar",
        string symbol = "$",
        bool isActive = true,
        bool isDeleted = false
    )
    {
        var existingCurrency = await _context.CurrencyCodes.FirstOrDefaultAsync(currency =>
            currency.Code == "Test USD"
        );

        if (existingCurrency is not null)
        {
            return existingCurrency;
        }

        var currency = new CurrencyCode
        {
            Id = currencyCodeId ?? Guid.NewGuid(),
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

    public async Task<Transaction> GetOrCreateTransactionAsync(
        TransactionType transactionType,
        Guid? userId = null,
        Guid? transactionId = null,
        Guid? accountId = null,
        long? transactionDate = null,
        decimal amount = 100m,
        bool isDeleted = false
    )
    {
        var existingAccount = await _context.Transactions.FirstOrDefaultAsync(transaction =>
            transaction.Id == transactionId && transaction.Type == transactionType
        );

        if (existingAccount is not null)
        {
            return existingAccount;
        }

        var user = await GetOrCreateUserAsync(userId);
        var account = await GetOrCreateAccountAsync(accountId: accountId, userId: user.Id);

        var transaction = new TransactionBuilder()
            .WithId(transactionId ?? Guid.NewGuid())
            .WithUser(user)
            .WithAccount(account)
            .WithAmount(amount)
            .WithDeletedStatus(isDeleted)
            .WithTransactionDate(transactionDate ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            .Build(transactionType);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> GetOrCreateExpenseAsync(
        Guid? userId = null,
        Guid? transactionId = null,
        Guid? accountId = null,
        long? transactionDate = null,
        decimal amount = 100m,
        bool isDeleted = false
    )
    {
        var transaction = await GetOrCreateTransactionAsync(
            TransactionType.Expense,
            transactionId: transactionId,
            accountId: accountId,
            userId: userId,
            transactionDate: transactionDate,
            amount: amount,
            isDeleted: isDeleted
        );

        return transaction;
    }

    public async Task<Transaction> GetOrCreateIncomeAsync(
        Guid? userId = null,
        Guid? transactionId = null,
        Guid? accountId = null,
        long? transactionDate = null,
        decimal amount = 100m,
        bool isDeleted = false
    )
    {
        var transaction = await GetOrCreateTransactionAsync(
            TransactionType.Income,
            transactionId: transactionId,
            accountId: accountId,
            userId: userId,
            transactionDate: transactionDate,
            amount: amount,
            isDeleted: isDeleted
        );

        return transaction;
    }
}
