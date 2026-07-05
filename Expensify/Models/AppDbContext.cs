using Microsoft.EntityFrameworkCore;

namespace Expensify.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}
