using Expensify.API.DTOs.AccountDTOs;
using Expensify.API.Services;
using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.IntergrationTests;

public class AccountServiceTests : IAsyncDisposable
{
    private readonly SqliteTestDatabase _database;
    private readonly TestDataSeeder _seeder;

    private readonly Mock<IAccountResolver> _accountResolverMock;
    private readonly Mock<IAccountTypeResolver> _accountTypeResolverMock;

    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _database = new SqliteTestDatabase();
        _seeder = new TestDataSeeder(_database.Context);

        _accountResolverMock = new Mock<IAccountResolver>();
        _accountTypeResolverMock = new Mock<IAccountTypeResolver>();

        _service = new AccountService(
            _database.Context,
            _accountTypeResolverMock.Object,
            _accountResolverMock.Object
        );
    }

    [Fact]
    public async Task CreateAccount_ValidRequest_CreatesAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();
        var currencyCodeId = Guid.NewGuid();

        await _seeder.GetOrCreateUserAsync(userId);
        await _seeder.GetOrCreateAccountTypeAsync(
            userId: userId,
            accountTypeId: accountTypeId,
            name: "Checking"
        );
        await _seeder.GetOrCreateCurrencyCodeAsync(currencyCodeId);

        Assert.True(
            await _database.Context.Users.AnyAsync(user => user.Id == userId),
            "The user was not seeded."
        );

        Assert.True(
            await _database.Context.AccountTypes.AnyAsync(accountType =>
                accountType.Id == accountTypeId
            ),
            "The account type was not seeded."
        );

        Assert.True(
            await _database.Context.CurrencyCodes.AnyAsync(currencyCode =>
                currencyCode.Id == currencyCodeId
            ),
            "The currency code was not seeded."
        );

        var request = CreateValidRequest(accountTypeId, currencyCodeId);

        var accountType = new AccountType { Id = accountTypeId, Name = "Checking" };

        _accountTypeResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountTypeById(
                    userId,
                    accountTypeId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(accountType);

        _accountResolverMock
            .Setup(resolver =>
                resolver.HasExistingAccountsByUserId(userId, It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(false);

        // Act
        var result = await _service.CreateAccount(userId, request, CancellationToken.None);

        AccountResponseDTO? response = null;
        Exception? error = null;

        var isSuccess = result.Match(
            success =>
            {
                response = success;
                return true;
            },
            exception =>
            {
                error = exception;
                return false;
            }
        );

        // Assert
        Assert.True(
            isSuccess,
            $"""
            Account creation failed.

            Exception:
            {error}
            """
        );

        Assert.Null(error);
        Assert.NotNull(response);

        var savedAccount = await _database.Context.Accounts.AsNoTracking().SingleAsync();

        Assert.Equal(userId, savedAccount.UserId);
        Assert.Equal(accountTypeId, savedAccount.AccountTypeId);
        Assert.Equal(currencyCodeId, savedAccount.CurrencyCodeId);
        Assert.Equal("Main Checking", savedAccount.Name);
        Assert.Equal("Chase", savedAccount.InstitutionName);
        Assert.Equal("1234", savedAccount.LastFourDigits);
        Assert.Equal(1000.00m, savedAccount.CurrentBalance);
        Assert.Equal(1000.00m, savedAccount.AvailableBalance);
        Assert.True(savedAccount.IncludeInNetWorth);
        Assert.True(savedAccount.IsActive);
        Assert.False(savedAccount.IsDeleted);
        Assert.True(savedAccount.IsDefault);

        _accountTypeResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountTypeById(
                    userId,
                    accountTypeId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );

        _accountResolverMock.Verify(
            resolver => resolver.HasExistingAccountsByUserId(userId, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    private static CreateAccountDTO CreateValidRequest(Guid accountTypeId, Guid currencyCodeId)
    {
        return new CreateAccountDTO
        {
            Name = "Main Checking",
            AccountTypeId = accountTypeId,
            CurrencyCodeId = currencyCodeId,
            InstitutionName = "Chase",
            LastFourDigits = "1234",
            InitialBalance = 1000.00m,
            IncludeInNetWorth = true,
            Notes = "Primary account",
            Icon = "🏦",
        };
    }

    public async ValueTask DisposeAsync()
    {
        await _database.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
