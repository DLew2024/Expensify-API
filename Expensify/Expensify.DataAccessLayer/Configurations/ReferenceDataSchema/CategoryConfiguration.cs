using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.DataAccessLayer.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.ReferenceDataSchema;

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
        // Maps categories to the reference_data schema.
        builder.ToTable("categories", Schemas.ReferenceData);

        // Configures the primary key.
        builder.HasKey(category => category.Id).HasName("pk_categories");

        // Configures the required category name.
        builder
            .Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the category type.
        builder.Property(category => category.Type).IsRequired();

        // Configures the optional description.
        builder
            .Property(category => category.Description)
            .HasMaxLength(DatabaseLengths.Description);

        // A category may belong to a user.
        // UserId is null for system-default categories.
        builder
            .HasOne(category => category.User)
            .WithMany(user => user.Categories)
            .HasForeignKey(category => category.UserId)
            .HasConstraintName("fk_categories_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One category can be assigned to many transactions.
        builder
            .HasMany(category => category.Transactions)
            .WithOne(transaction => transaction.Category)
            .HasForeignKey(transaction => transaction.CategoryId)
            .HasConstraintName("fk_transactions_categories_category_id")
            .OnDelete(DeleteBehavior.SetNull);

        // One category can participate in many budget-category relationships.
        builder
            .HasMany(category => category.BudgetCategories)
            .WithOne(budgetCategory => budgetCategory.Category)
            .HasForeignKey(budgetCategory => budgetCategory.CategoryId)
            .HasConstraintName("fk_budget_categories_categories_category_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Prevents duplicate user-created category names of the same type.
        builder
            .HasIndex(category => new
            {
                category.UserId,
                category.Name,
                category.Type,
            })
            .IsUnique()
            .HasDatabaseName("ux_categories_user_id_name_type");

        // Improves lookups for active categories by user and type.
        builder
            .HasIndex(category => new
            {
                category.UserId,
                category.Type,
                category.IsActive,
            })
            .HasDatabaseName("ix_categories_user_id_type_is_active");
    }
}
