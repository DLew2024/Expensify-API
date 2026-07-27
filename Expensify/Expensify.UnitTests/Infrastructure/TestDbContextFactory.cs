using Expensify.DataAccessLayer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Expensify.UnitTests.Infrastructure;

public static class TestDbContextFactory
{
    public static ApplicationDbContext Create(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        return new ApplicationDbContext(options);
    }
}
