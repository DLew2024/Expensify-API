using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Expensify.DataAccessLayer;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public virtual DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    ///  public virtual DbSet<Account> Accounts { get; set; }
    ///  public virtual DbSet<Category> Categories { get; set; }
    ///  public virtual DbSet<Budget> Budgets { get; set; }
    ///  public virtual DbSet<BudgetMember> BudgetMembers { get; set; }
    ///  public virtual DbSet<BudgetCategory> BudgetCategories { get; set; }
    ///  public virtual DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    ///  public virtual DbSet<Goal> Goals { get; set; }
    ///  public virtual DbSet<AccountType> AccountTypes { get; set; }
    ///  public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    ///  public virtual DbSet<GoalType> GoalTypes { get; set; }
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
