using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.DataAccessLayer.Utility;
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
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Budget> Budgets { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<CurrencyCode> CurrencyCodes { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<AccountType> AccountTypes { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    /// <summary>
    ///  public virtual DbSet<BudgetMember> BudgetMembers { get; set; }
    ///  public virtual DbSet<BudgetCategory> BudgetCategories { get; set; }
    ///  public virtual DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    ///  public virtual DbSet<Goal> Goals { get; set; }
    ///  public virtual DbSet<GoalType> GoalTypes { get; set; }
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Public);
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // For unit tests
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            modelBuilder
                .Entity<Account>()
                .HasIndex(account => account.UserId)
                .IsUnique()
                .HasFilter("\"is_default\" = 1")
                .HasDatabaseName("ux_accounts_user_default");
        }

        // For running app
        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
        {
            // Uses PostgreSQL's xmin system column for optimistic concurrency control.
            // This helps prevent conflicting updates from silently overwriting each other.
            modelBuilder.Entity<Account>().Property<uint>("xmin").IsRowVersion();
        }
    }
}
