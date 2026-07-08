using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations;

/// <summary>
/// Configures the database mapping for the <see cref="User"/> entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the <see cref="User"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id).HasName("pk_users");

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(320);

        builder.HasIndex(x => x.Email).IsUnique().HasDatabaseName("ux_users_email");

        builder.Property(x => x.Password).IsRequired().HasMaxLength(500);

        builder.Property(x => x.ProfileImageUrl).HasMaxLength(2048);

        builder
            .HasMany(x => x.Accounts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_accounts_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Transactions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_transactions_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Categories)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_categories_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.OwnedBudgets)
            .WithOne(x => x.OwnerUser)
            .HasForeignKey(x => x.OwnerUserId)
            .HasConstraintName("fk_budgets_users_owner_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.BudgetMemberships)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_budget_memberships_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.RecurringTransactions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_recurring_transactions_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.PasswordResetTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_password_reset_tokens_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
