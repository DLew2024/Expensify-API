using Expensify.API.Services.Resolvers;
using Expensify.DataAccessLayer.Enums;
using Expensify.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.IntergrationTests.Reslovers;

public sealed class AccountResolverTests : IAsyncDisposable
{
    private readonly SqliteTestDatabase _database;
    private readonly TestDataSeeder _seeder;
    private readonly AccountResolver _resolver;

    public AccountResolverTests()
    {
        _database = new SqliteTestDatabase();
        _seeder = new TestDataSeeder(_database.Context);
        _resolver = new AccountResolver(_database.Context);
    }

    // MethodName_StateUnderTest_ExpectedBehavior
    // Arrange
    // Act
    // Assert

    // ResolveAccountIdByUserId
    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsAccountId_WhenAccountIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: userId);

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountId, result);
    }

    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsNull_WhenAccountDoesNotExist()
    {
        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsNull_WhenAccountBelongsToAnotherUser()
    {
        // Arrange
        var accountOwnerId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: accountOwnerId);

        var requestingUserId = Guid.NewGuid();

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            requestingUserId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsNull_WhenAccountIsInactive()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: false
        );

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsNull_WhenAccountIsSoftDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    // ResolveAccountByUserId
    [Fact]
    public async Task ResolveAccountByUserId_ReturnsAccount_WhenAccountIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var userAccount = await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveAccountByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userAccount.Id, result.Id);
    }

    [Fact]
    public async Task ResolveAccountByUserId_ReturnsNull_WhenAccountDoesNotExist()
    {
        // Act
        var result = await _resolver.ResolveAccountByUserId(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserId_ReturnsNull_WhenAccountBelongsToAnotherUser()
    {
        // Arrange
        var accountOwnerId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: accountOwnerId);

        var requestingUserId = Guid.NewGuid();

        // Act
        var result = await _resolver.ResolveAccountByUserId(
            requestingUserId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserId_ReturnsNull_WhenAccountIsInactive()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: false
        );

        // Act
        var result = await _resolver.ResolveAccountByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserId_ReturnsNull_WhenAccountIsSoftDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    // ResolveAccountByUserIdWithTracking
    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsTrackedAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: userId);

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);

        var entry = _database.Context.Entry(result);

        Assert.Equal(EntityState.Unchanged, entry.State);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_PersistsChanges_WhenEntityIsModified()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: userId);

        // Act
        var trackedAccount = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        Assert.NotNull(trackedAccount);

        trackedAccount.Name = "Updated Account";

        await _database.Context.SaveChangesAsync();

        // Assert
        var updatedAccount = await _database
            .Context.Accounts.AsNoTracking()
            .FirstAsync(account => account.Id == accountId);

        Assert.Equal("Updated Account", updatedAccount.Name);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsAccount_WhenAccountIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        var expectedAccount = await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: true,
            isDeleted: false
        );

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAccount.Id, result.Id);
        Assert.Equal(expectedAccount.UserId, result.UserId);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsNull_WhenAccountDoesNotExist()
    {
        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsNull_WhenAccountBelongsToAnotherUser()
    {
        // Arrange
        var accountOwnerId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(accountId: accountId, userId: accountOwnerId);

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            requestingUserId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsNull_WhenAccountIsInactive()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: false
        );

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsNull_WhenAccountIsSoftDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByUserIdWithTracking_ReturnsNull_WhenAccountIsInactiveAndSoftDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: false,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountByUserIdWithTracking(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    // ResolveAccountByTransactionId
    [Fact]
    public async Task ResolveAccountByTransactionId_ValidTransactionValidUser_ReturnsAccount()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        var transaction = await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            userId,
            transactionId,
            cancellationToken: CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.AccountId, result.Id);
        Assert.Equal(result.UserId, userId);
    }

    [Fact]
    public async Task ResolveAccountByTransactionId_TransactionDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByTransactionId_InvalidTransactionId_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();
        var invalidTransactionId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            userId,
            invalidTransactionId,
            cancellationToken: CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByTransactionId_TransactionBelongsToAnotherUser_ReturnsNull()
    {
        // Arrange
        var transactionOwnerId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: transactionOwnerId
        );

        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            requestingUserId,
            transactionId,
            cancellationToken: CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByTransactionId_TransactionIsSoftDeleted_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            userId,
            transactionId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountByTransactionId_AccountIsSoftDeleted_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        var account = await _seeder.GetOrCreateAccountAsync(userId: userId, isDeleted: true);

        await _seeder.GetOrCreateTransactionAsync(
            transactionType: TransactionType.Income,
            transactionId: transactionId,
            userId: userId,
            accountId: account.Id
        );

        // Act
        var result = await _resolver.ResolveAccountByTransactionId(
            userId,
            transactionId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    public async ValueTask DisposeAsync()
    {
        await _database.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
