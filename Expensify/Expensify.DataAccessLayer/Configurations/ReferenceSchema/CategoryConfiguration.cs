using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.ReferenceSchema;

/// <summary>
/// Configures the database mapping for the <see cref="Category"/> entity.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Configures the <see cref="Category"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Maps the entity to the categories table
        // in the reference schema.
        builder.ToTable("categories", "reference");

        // Configures the primary key.
        builder.HasKey(category => category.Id)
            .HasName("pk_categories");

        // Configures the required category name.
        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the optional category description.
        builder.Property(category => category.Description)
            .HasMaxLength(DatabaseLengths.Description);

        // Configures the active-status flag.
        builder.Property(category => category.IsActive)
            .IsRequired();

        // Configures the system-default flag.
        builder.Property(category => category.IsSystemDefault)
            .IsRequired();

        // One user can create many categories.
        builder.HasOne(category => category.User)
            .WithMany(user => user.Categories)
            .HasForeignKey(category => category.UserId)
            .HasConstraintName("fk_categories_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One category can be assigned to many transactions.
        // Restrict prevents deleting a category that is still
        // referenced by transaction history.
        builder.HasMany(category => category.Transactions)
            .WithOne(transaction => transaction.Category)
            .HasForeignKey(transaction => transaction.CategoryId)
            .HasConstraintName("fk_transactions_categories_category_id")
            .OnDelete(DeleteBehavior.Restrict);

        // One category can belong to many budget-category relationships.
        builder.HasMany(category => category.BudgetCategories)
            .WithOne(budgetCategory => budgetCategory.Category)
            .HasForeignKey(budgetCategory => budgetCategory.CategoryId)
            .HasConstraintName("fk_budget_categories_categories_category_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Prevents a user from creating duplicate category names.
        builder.HasIndex(category => new
        {
            category.UserId,
            category.Name
        })
        .IsUnique()
        .HasDatabaseName("ux_categories_user_id_name");

        // Improves queries that retrieve active categories for a user.
        builder.HasIndex(category => new
        {
            category.UserId,
            category.IsActive
        })
        .HasDatabaseName("ix_categories_user_id_is_active");
    }
}