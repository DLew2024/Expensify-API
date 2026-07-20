using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.BudgetingSchema;

/// <summary>
/// Configures the database mapping for the <see cref="Budget"/> entity.
/// </summary>
public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    /// <summary>
    /// Configures the <see cref="Budget"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        // Maps the entity to the budgets table in the budgeting schema.
        builder.ToTable("budgets", "budgeting");

        // Configures the primary key.
        builder.HasKey(budget => budget.Id).HasName("pk_budgets");

        // Configures the required budget name.
        builder.Property(budget => budget.Name).IsRequired().HasMaxLength(DatabaseLengths.Name);

        // Configures the optional budget description.
        builder.Property(budget => budget.Description).HasMaxLength(DatabaseLengths.Description);

        // Configures the total budget limit.
        builder.Property(budget => budget.LimitAmount).IsRequired().HasPrecision(18, 2);

        // Stores the budget period enum as an integer.
        builder.Property(budget => budget.Period).IsRequired();

        // Configures the required budget start date.
        builder.Property(budget => budget.StartDate).IsRequired();

        // Configures the active-status flag.
        builder.Property(budget => budget.IsActive).IsRequired();

        // Configures the shared-budget flag.
        builder.Property(budget => budget.IsShared).IsRequired();

        // One user can own many budgets.
        builder
            .HasOne(budget => budget.OwnerUser)
            .WithMany(user => user.OwnedBudgets)
            .HasForeignKey(budget => budget.OwnerUserId)
            .HasConstraintName("fk_budgets_users_owner_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One budget can contain many category assignments.
        builder
            .HasMany(budget => budget.BudgetCategories)
            .WithOne(budgetCategory => budgetCategory.Budget)
            .HasForeignKey(budgetCategory => budgetCategory.BudgetId)
            .HasConstraintName("fk_budget_categories_budgets_budget_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One budget can contain many shared members.
        builder
            .HasMany(budget => budget.Members)
            .WithOne(member => member.Budget)
            .HasForeignKey(member => member.BudgetId)
            .HasConstraintName("fk_budget_members_budgets_budget_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Improves queries that retrieve active budgets owned by a user.
        builder
            .HasIndex(budget => new { budget.OwnerUserId, budget.IsActive })
            .HasDatabaseName("ix_budgets_owner_user_id_is_active");

        // Prevents a user from creating duplicate budget names.
        builder
            .HasIndex(budget => new { budget.OwnerUserId, budget.Name })
            .IsUnique()
            .HasDatabaseName("ux_budgets_owner_user_id_name");
    }
}
