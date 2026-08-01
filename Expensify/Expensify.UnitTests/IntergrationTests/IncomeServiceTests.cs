using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.UnitTests.Infrastructure;

namespace Expensify.UnitTests.IntergrationTests;

public class IncomeServiceTests : IAsyncDisposable
{
    private readonly SqliteTestDatabase _database;
    private readonly TestDataSeeder _seeder;
    private readonly Mock<IAccountResolver> _accountResolver;
    private readonly Mock<ITransactionResolver> _transactionResolver;

    public IncomeServiceTests()
    {
       _database = new SqliteTestDatabase();
        _seeder = new TestDataSeeder(_database.Context);
        _accountResolver = new Mock<IAccountResolver>();
        _transactionResolver = new Mock<ITransactionResolver>();
    }

    public async ValueTask DisposeAsync()
    {
        await _database.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
