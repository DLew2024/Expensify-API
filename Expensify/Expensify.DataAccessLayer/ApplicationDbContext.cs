using Expensify.Models;
using Microsoft.EntityFrameworkCore;

namespace Expensify.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
        }
    }
}
