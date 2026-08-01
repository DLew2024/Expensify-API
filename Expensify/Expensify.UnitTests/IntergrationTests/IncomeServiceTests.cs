using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.DataAccessLayer;
using Expensify.UnitTests.Infrastructure;

namespace Expensify.UnitTests.IntergrationTests;

public class IncomeServiceTests : IAsyncDisposable
{
    private readonly DatabaseSqlLite;
    private readonly TestDataSeeder _dataSeeder;
    private readonly Mock<IAccountResolver> _accountResolver;
    private readonly Mock<ITransactionResolver> _transactionResolver;

    public IncomeServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _accountResolver = new Mock<IAccountResolver>();
        _transactionResolver = new Mock<ITransactionResolver>();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
