using Expensify.DataAccessLayer;
using Microsoft.Data.Sqlite;

namespace Expensify.UnitTests.Infrastructure;

public sealed class SqliteTestDatabase : IAsyncDisposable
{
    private readonly SqliteConnection _connection;

    public ApplicationDbContext Context { get; }

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        Context = TestDbContextFactory.Create(_connection);

        Context.Database.EnsureCreated();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        await _connection.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}
