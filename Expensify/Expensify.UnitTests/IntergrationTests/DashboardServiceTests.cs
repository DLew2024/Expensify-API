using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.Services;
using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.UnitTests.IntergrationTests;

public class DashboardServiceTests
{
    private readonly Mock<IAccountResolver> _accountResolverMock;
    private readonly Mock<ITransactionResolver> _transactionResolverMock;
    private readonly DashboardService _service;

    public DashboardServiceTests()
    {
        _accountResolverMock = new Mock<IAccountResolver>();
        _transactionResolverMock = new Mock<ITransactionResolver>();

        _service = new DashboardService(
            _accountResolverMock.Object,
            _transactionResolverMock.Object
        );
    }

    [Fact]
    public async Task GetDashboardData_ValidAccount_ReturnsDashboardData()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var account = new Account
        {
            Id = accountId,
            UserId = userId,
            Name = "Checking",
            CurrentBalance = 1500.00m,
        };

        var transactions = new[]
        {
            new TransactionDTO { Id = Guid.NewGuid(), Amount = 100.00m },
            new TransactionDTO { Id = Guid.NewGuid(), Amount = 50.00m },
        };

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserId(userId, accountId, It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(account);

        _transactionResolverMock
            .Setup(resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(transactions);

        // Act
        var result = await _service.GetDashboardData(userId, accountId, CancellationToken.None);

        // Assert
        DashboardDataResponseDTO? response = null;
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
        Assert.True(isSuccess);
        Assert.NotNull(response);
        Assert.Null(error);

        Assert.Equal(accountId, response.Account?.Id);
        Assert.Equal("Checking", response.Account?.Name);
        Assert.Equal(1500.00m, response.TotalBalance);

        _accountResolverMock.Verify(
            resolver =>
                resolver.ResolveAccountByUserId(userId, accountId, It.IsAny<CancellationToken>()),
            Times.Once
        );

        _transactionResolverMock.Verify(
            resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public async Task GetDashboardData_AccountNotFound_ReturnsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserId(userId, accountId, It.IsAny<CancellationToken>())
            )
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _service.GetDashboardData(userId, accountId, CancellationToken.None);

        // Assert
        Exception? error = null;

        var isSuccess = result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

        Assert.False(isSuccess);
        Assert.IsType<EntityNotFoundException>(error);

        _transactionResolverMock.Verify(
            resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                ),
            Times.Never
        );
    }

    [Fact]
    public async Task GetDashboardData_AccountResolverThrows_ReturnsExceptionResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var expectedException = new InvalidOperationException("Database error");

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserId(userId, accountId, It.IsAny<CancellationToken>())
            )
            .ThrowsAsync(expectedException);

        // Act
        var result = await _service.GetDashboardData(userId, accountId, CancellationToken.None);

        // Assert
        Exception? error = null;

        var isSuccess = result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

        Assert.False(isSuccess);
        Assert.Same(expectedException, error);

        _transactionResolverMock.Verify(
            resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                ),
            Times.Never
        );
    }

    [Fact]
    public async Task GetDashboardData_TransactionResolverThrows_ReturnsExceptionResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var account = new Account
        {
            Id = accountId,
            UserId = userId,
            Name = "Checking",
            CurrentBalance = 1500.00m,
        };

        var expectedException = new InvalidOperationException("Database error");

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserId(userId, accountId, It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(account);

        _transactionResolverMock
            .Setup(resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    userId,
                    accountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ThrowsAsync(expectedException);

        // Act
        var result = await _service.GetDashboardData(userId, accountId, CancellationToken.None);

        // Assert
        Exception? error = null;

        var isSuccess = result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

        Assert.False(isSuccess);
        Assert.Same(expectedException, error);
    }

    [Fact]
    public async Task GetDashboardData_AccountResolved_UsesResolvedAccountIdForTransactions()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var requestedAccountId = Guid.NewGuid();
        var resolvedAccountId = Guid.NewGuid();

        var account = new Account
        {
            Id = resolvedAccountId,
            UserId = userId,
            Name = "Checking",
            CurrentBalance = 1000.00m,
        };

        _accountResolverMock
            .Setup(resolver =>
                resolver.ResolveAccountByUserId(
                    userId,
                    requestedAccountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(account);

        _transactionResolverMock
            .Setup(resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    userId,
                    resolvedAccountId,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync([]);

        // Act
        await _service.GetDashboardData(userId, requestedAccountId, CancellationToken.None);

        // Assert
        _transactionResolverMock.Verify(
            resolver =>
                resolver.ResolveTransactionsByUserAndAccount(
                    userId,
                    resolvedAccountId,
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact]
    public void Create_TransactionsProvided_CalculatesDashboardDataCorrectly()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;

        var account = new AccountSummaryDTO
        {
            Id = Guid.NewGuid(),
            Name = "Checking",
            CurrentBalance = 1250.00m,
        };

        var recentIncome = new TransactionDTO
        {
            Id = Guid.NewGuid(),
            Amount = 500.00m,
            Type = TransactionType.Income,
            TransactionDate = now.AddDays(-5).ToUnixTimeSeconds(),
            Merchant = "Employer",
        };

        var recentExpense = new TransactionDTO
        {
            Id = Guid.NewGuid(),
            Amount = 125.00m,
            Type = TransactionType.Expense,
            TransactionDate = now.AddDays(-10).ToUnixTimeSeconds(),
            Merchant = "Grocery Store",
        };

        var olderExpense = new TransactionDTO
        {
            Id = Guid.NewGuid(),
            Amount = 75.00m,
            Type = TransactionType.Expense,
            TransactionDate = now.AddDays(-45).ToUnixTimeSeconds(),
            Merchant = "Utility Company",
        };

        var expiredIncome = new TransactionDTO
        {
            Id = Guid.NewGuid(),
            Amount = 200.00m,
            Type = TransactionType.Income,
            TransactionDate = now.AddDays(-70).ToUnixTimeSeconds(),
            Merchant = "Previous Employer",
        };

        TransactionDTO[] transactions = [olderExpense, recentIncome, expiredIncome, recentExpense];

        // Act
        var result = DashboardDataResponseDTO.Create(account, transactions);

        // Assert
        Assert.Equal(1250.00m, result.TotalBalance);

        Assert.Equal(700.00m, result.TotalIncome);
        Assert.Equal(200.00m, result.TotalExpenses);

        Assert.NotNull(result.Account);
        Assert.Equal(account.Id, result.Account.Id);
        Assert.Equal("Checking", result.Account.Name);

        Assert.NotNull(result.Last30DaysOfIncome);
        Assert.Equal(500.00m, result.Last30DaysOfIncome.TotalBalance);
        Assert.Single(result.Last30DaysOfIncome.Transactions);
        Assert.Equal(recentIncome.Id, result.Last30DaysOfIncome.Transactions[0].Id);

        Assert.NotNull(result.Last60DaysOfIncome);
        Assert.Equal(500.00m, result.Last60DaysOfIncome.TotalBalance);
        Assert.Single(result.Last60DaysOfIncome.Transactions);

        Assert.NotNull(result.Last30DaysOfExpenses);
        Assert.Equal(125.00m, result.Last30DaysOfExpenses.TotalBalance);
        Assert.Single(result.Last30DaysOfExpenses.Transactions);
        Assert.Equal(recentExpense.Id, result.Last30DaysOfExpenses.Transactions[0].Id);

        Assert.NotNull(result.Last60DaysOfExpenses);
        Assert.Equal(200.00m, result.Last60DaysOfExpenses.TotalBalance);
        Assert.Equal(2, result.Last60DaysOfExpenses.Transactions.Length);

        Assert.Equal(4, result.RecentTransactions.Length);

        Assert.Equal(recentIncome.Id, result.RecentTransactions[0].Id);

        Assert.Equal(recentExpense.Id, result.RecentTransactions[1].Id);

        Assert.Equal(olderExpense.Id, result.RecentTransactions[2].Id);

        Assert.Equal(expiredIncome.Id, result.RecentTransactions[3].Id);
    }
}
