using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.Services;
using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;
using Expensify.UnitTests.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.IntergrationTests;

public class IncomeServiceTests : IAsyncDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ApplicationDbContext _context;
    private readonly TestDataSeeder _seeder;
    private readonly Mock<IAccountResolver> _accountResolverMock;
    private readonly Mock<ITransactionResolver> _transactionResolver;
    private readonly IncomeService _service;

    public IncomeServiceTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();

        _seeder = new TestDataSeeder(_context);

        _transactionResolver = new Mock<ITransactionResolver>();
        _accountResolverMock = new Mock<IAccountResolver>();

        _service = new IncomeService(
            _context,
            _accountResolverMock.Object,
            _transactionResolver.Object
        );
    }

    public async ValueTask DisposeAsync()
    {
        _context.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    // MethodName_StateUnderTest_ExpectedBehavior
    // Arrange
    // Act
    // Assert
    [Fact]
    public async Task AddIncome_WhenValidIncomeIsProvided_AddsIncomeToDatabase()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var paymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 100m
        );

        await _seeder.GetOrCreatePaymentMethodAsync(
            paymentMethodId: paymentMethodId,
            name: "Test Payment Method"
        );

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = new AddIncomeTransactionDTO
        {
            AccountId = accountId,
            PaymentMethodId = paymentMethodId,
            Amount = 50m,
            Source = "Employer",
            Description = "Paycheck",
            TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Icon = "💰",
            Notes = string.Empty,
        };

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        var transaction = await _context.Transactions.AsNoTracking().SingleAsync();

        Assert.Equal(userId, transaction.UserId);
        Assert.Equal(accountId, transaction.AccountId);
        Assert.Equal(paymentMethodId, transaction.PaymentMethodId);
        Assert.Equal(50m, transaction.Amount);
        Assert.Equal(150m, transaction.AccountBalanceAfterTransaction);
        Assert.Equal(TransactionType.Income, transaction.Type);
        Assert.Equal(TransactionPostedStatus.Posted, transaction.Status);

        var updatedAccount = await _context
            .Accounts.AsNoTracking()
            .SingleAsync(savedAccount => savedAccount.Id == accountId);

        Assert.Equal(150m, updatedAccount.CurrentBalance);
        Assert.Equal(150m, updatedAccount.AvailableBalance);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task AddIncome_WhenValidIncomeIsProvided_UpdatesAccountBalance()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var paymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 400m
        );

        await _seeder.GetOrCreatePaymentMethodAsync(
            paymentMethodId: paymentMethodId,
            name: "Test Payment Method"
        );

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, paymentMethodId, amount: 100m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        var updatedAccount = await _context
            .Accounts.AsNoTracking()
            .SingleAsync(savedAccount =>
                savedAccount.Id == accountId && savedAccount.UserId == userId
            );

        Assert.Equal(500m, updatedAccount.CurrentBalance);
        Assert.Equal(500m, updatedAccount.AvailableBalance);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task AddIncome_WhenAmountIsNotGreaterThanZero_ReturnsValidationException(
        decimal amount
    )
    {
        // Arrange
        var request = CreateValidRequest(Guid.NewGuid(), Guid.NewGuid(), amount);

        // Act
        var result = await _service.AddIncome(Guid.NewGuid(), request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        Exception? actualException = null;
        result.IfFail(ex => actualException = ex);

        var exception = Assert.IsType<ValidationException>(actualException);

        Assert.Equal("The income amount must be greater than zero.", exception.Message);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                ),
            Times.Never
        );
    }

    [Fact]
    public async Task AddIncome_WhenAccountDoesNotExist_ReturnsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Account?)null);

        var request = CreateValidRequest(accountId, Guid.NewGuid(), 100m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        Exception? actualException = null;
        result.IfFail(exception => actualException = exception);

        var exception = Assert.IsType<EntityNotFoundException>(actualException);

        Assert.Equal("The requested account could not be found.", exception.Message);

        Assert.Empty(_context.Transactions);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task AddIncome_WhenAccountIsClosed_ReturnsValidationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 500m,
            closedDate: DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        );

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, Guid.NewGuid(), 100m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        Exception? actualException = null;
        result.IfFail(ex => actualException = ex);

        var exception = Assert.IsType<ValidationException>(actualException);

        Assert.Equal("Transactions cannot be added to a closed account.", exception.Message);

        Assert.Empty(_context.Transactions);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task AddIncome_WhenPaymentMethodDoesNotExist_ReturnsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var missingPaymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 500m
        );

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, missingPaymentMethodId, 100m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        var actualException = result.Match<Exception?>(_ => null, exception => exception);

        var entityNotFoundException = Assert.IsType<EntityNotFoundException>(actualException);

        Assert.Equal(
            "The requested payment method could not be found.",
            entityNotFoundException.Message
        );

        Assert.Empty(_context.Transactions);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task AddIncome_WhenPaymentMethodIsDeleted_ReturnsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var paymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 500m
        );

        await _seeder.GetOrCreatePaymentMethodAsync(
            paymentMethodId: paymentMethodId,
            isDeleted: true
        );

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, paymentMethodId, 100m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);

        Exception? actualException = null;

        result.IfFail(exception => actualException = exception);

        var exception = Assert.IsType<EntityNotFoundException>(actualException);

        Assert.Equal("The requested payment method could not be found.", exception.Message);

        Assert.Empty(_context.Transactions);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task AddIncome_WhenSuccessful_ReturnsCreatedTransaction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var paymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 200m
        );

        await _seeder.GetOrCreatePaymentMethodAsync(paymentMethodId: paymentMethodId);

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, paymentMethodId, 300m);

        // Act
        var result = await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        IncomeTransactionResponseDTO? response = null;
        result.IfSucc(value => response = value);

        Assert.NotNull(response);
        Assert.Equal(300m, response.Amount);
    }

    [Fact]
    public async Task AddIncome_WhenSuccessful_CallsAccountResolverOnce()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var paymentMethodId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(
            userId: userId,
            accountId: accountId,
            initialBalance: 200m
        );

        await _seeder.GetOrCreatePaymentMethodAsync(paymentMethodId: paymentMethodId);

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        var request = CreateValidRequest(accountId, paymentMethodId, 100m);

        // Act
        await _service.AddIncome(userId, request, CancellationToken.None);

        // Assert
        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserIdWithTracking(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    private static AddIncomeTransactionDTO CreateValidRequest(
        Guid accountId,
        Guid paymentMethodId,
        decimal amount
    )
    {
        return new AddIncomeTransactionDTO
        {
            AccountId = accountId,
            PaymentMethodId = paymentMethodId,
            Amount = amount,
            Source = "Employer",
            Description = "Income transaction",
            TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Icon = "💰",
            Notes = string.Empty,
        };
    }
}
