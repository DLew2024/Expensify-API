using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations.BudgetingSchema;

/// <summary>
/// Configures the database mapping for the <see cref="BudgetCategory"/> entity.
/// </summary>
public class BudgetCategoryConfiguration : IEntityTypeConfiguration<BudgetCategory>
{
    /// <summary>
    /// Configures the <see cref="BudgetCategory"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<BudgetCategory> builder)
    {
        // Maps the entity to the budget_categories table
        // in the budgeting schema.
        builder.ToTable("budget_categories", "budgeting");

        // Configures the primary key.
        builder.HasKey(budgetCategory => budgetCategory.Id);

        // Configures the optional category spending limit.
        builder.Property(budgetCategory => budgetCategory.CategoryLimitAmount).HasPrecision(18, 2);

        // One budget can contain many category assignments.
        builder
            .HasOne(budgetCategory => budgetCategory.Budget)
            .WithMany(budget => budget.BudgetCategories)
            .HasForeignKey(budgetCategory => budgetCategory.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        // One category can be assigned to many budgets.
        builder
            .HasOne(budgetCategory => budgetCategory.Category)
            .WithMany(category => category.BudgetCategories)
            .HasForeignKey(budgetCategory => budgetCategory.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prevents the same category from being added to a budget more than once.
        // This index also supports queries filtered by BudgetId.
        builder
            .HasIndex(budgetCategory => new { budgetCategory.BudgetId, budgetCategory.CategoryId })
            .IsUnique()
            .HasDatabaseName("ux_budget_categories_budget_id_category_id");

        // Improves queries that locate all budgets using a category.
        builder
            .HasIndex(budgetCategory => budgetCategory.CategoryId)
            .HasDatabaseName("ix_budget_categories_category_id");
    }
}
