using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

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

        result.Match(
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
        Assert.Null(error);
        Assert.NotNull(response);

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

        result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

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

        result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

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

        result.Match(
            success => true,
            exception =>
            {
                error = exception;
                return false;
            }
        );

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
}
