using Expensify.API.ServiceClasses.Resolvers;
using Expensify.UnitTests.Infrastructure;

namespace Expensify.UnitTests.IntergrationTests.Reslovers;

public class AccountTypeResolverTests
{
    private readonly SqliteTestDatabase _database;
    private readonly TestDataSeeder _seeder;
    private readonly AccountTypeResolver _resolver;

    public AccountTypeResolverTests()
    {
        _database = new SqliteTestDatabase();
        _seeder = new TestDataSeeder(_database.Context);
        _resolver = new AccountTypeResolver(_database.Context);
    }

    // MethodName_StateUnderTest_ExpectedBehavior
    // Arrange
    // Act
    // Assert
    [Fact]
    public async Task ResolveAccountTypeById_SystemDefaultAccountType_ReturnsAccountType()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();

        var accountType = await _seeder.GetOrCreateAccountTypeAsync(
            userId: userId,
            accountTypeId: accountTypeId,
            isSystemDefault: true
        );

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            userId,
            accountTypeId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountType.Id, result.Id);
    }

    [Fact]
    public async Task ResolveAccountTypeById_UserOwnedAccountType_ReturnsAccountType()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();

        var accountType = await _seeder.GetOrCreateAccountTypeAsync(
            accountTypeId: accountTypeId,
            userId: userId
        );

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            userId,
            accountTypeId,
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountType.Id, result.Id);
        Assert.Equal(userId, result.UserId);
    }

    [Fact]
    public async Task ResolveAccountTypeById_AccountTypeDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _resolver.ResolveAccountTypeById(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountTypeById_AccountTypeBelongsToAnotherUser_ReturnsNull()
    {
        // Arrange
        var ownerUserId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountTypeAsync(
            accountTypeId: accountTypeId,
            userId: ownerUserId
        );

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            requestingUserId,
            accountTypeId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountTypeById_AccountTypeIsInactive_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountTypeAsync(
            accountTypeId: accountTypeId,
            userId: userId,
            isActive: false
        );

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            userId,
            accountTypeId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ResolveAccountTypeById_AccountTypeIsSoftDeleted_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accountTypeId = Guid.NewGuid();

        await _seeder.GetOrCreateAccountTypeAsync(
            accountTypeId: accountTypeId,
            userId: userId,
            isDeleted: true
        );

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            userId,
            accountTypeId,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }
}
