using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Expensify.DataAccessLayer
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options)
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
        }
    }
}
