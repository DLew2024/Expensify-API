using Expensify.API.ServiceClasses.Resolvers;
using Expensify.UnitTests.Infrastructure;

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
    [Fact]
    public async Task ResolveAccountIdByUserId_ReturnsAccountId_WhenAccountIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();

        await _seeder.CreateAccountAsync(
            accountId: accountId,
            userId: userId,
            isActive: true,
            isDeleted: false
        );

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            userId,
            accountId,
            CancellationToken.None
        );

        // Assert
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

        await _seeder.CreateAccountAsync(accountId: accountId, userId: accountOwnerId);

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

        await _seeder.CreateAccountAsync(accountId: accountId, userId: userId, isActive: false);

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

        await _seeder.CreateAccountAsync(accountId: accountId, userId: userId, isDeleted: true);

        // Act
        var result = await _resolver.ResolveAccountIdByUserId(
            userId,
            accountId,
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
