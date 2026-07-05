using Expensify.Models;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Income> Incomes => Set<Income>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
        }
    }
}
