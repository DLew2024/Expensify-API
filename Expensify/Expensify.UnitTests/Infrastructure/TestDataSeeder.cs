using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.Infrastructure
{
    public sealed class TestDataSeeder(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Role> GetOrCreateUserRoleAsync()
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(role =>
                role.Name == "User"
            );

            if (existingRole is not null)
            {
                return existingRole;
            }

            var role = new Role { Id = Guid.NewGuid(), Name = "User" };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<CurrencyCode> GetOrCreateUsdCurrencyAsync()
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
                Name = "US Dollar",
                Code = "USD",
                Symbol = "$",
                IsActive = true,
                IsDeleted = false,
            };

            _context.CurrencyCodes.Add(currency);
            await _context.SaveChangesAsync();

            return currency;
        }

        public async Task<AccountType> GetOrCreateCheckingAccountTypeAsync(Guid userId)
        {
            var existingAccountType = await _context.AccountTypes.FirstOrDefaultAsync(accountType =>
                accountType.Name == "Checking"
                && accountType.IsSystemDefault
                && !accountType.IsDeleted
            );

            if (existingAccountType is not null)
            {
                return existingAccountType;
            }

            var accountType = new AccountType
            {
                Id = Guid.NewGuid(),
                Name = "Checking",
                IsActive = true,
                IsDeleted = false,
                IsSystemDefault = true,
                CreatedBy = userId,
                LastUpdatedBy = userId,
            };

            _context.AccountTypes.Add(accountType);
            await _context.SaveChangesAsync();

            return accountType;
        }

        public async Task<User> CreateUserAsync(Guid? userId = null)
        {
            var role = await GetOrCreateUserRoleAsync();

            var user = new UserBuilder().WithId(userId ?? Guid.NewGuid()).WithRole(role).Build();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Account> CreateAccountAsync(
            Guid? accountId = null,
            Guid? userId = null,
            bool isActive = true,
            bool isDeleted = false,
            bool isDefault = false
        )
        {
            var user = await CreateUserAsync(userId);
            var currency = await GetOrCreateUsdCurrencyAsync();
            var accountType = await GetOrCreateCheckingAccountTypeAsync(user.Id);

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
    }
}
