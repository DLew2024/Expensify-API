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
    public async Task ResolveAccountTypeById_InvalidUserIdForNonDefaultAcccountType_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var requestingUserId = Guid.NewGuid();

        var accountType = await _seeder.GetOrCreateAccountTypeAsync(userId, isSystemDefault: false);

        // Act
        var result = await _resolver.ResolveAccountTypeById(
            requestingUserId,
            accountType.Id,
            CancellationToken.None
        );

        // Assert
        Assert.Null(result);
    }
}
