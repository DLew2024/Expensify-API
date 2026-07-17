using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

/// <summary>
/// Configures the database mapping for the <see cref="AccountType"/> entity.
/// </summary>
public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
{
    /// <summary>
    /// Configures the <see cref="AccountType"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        // Maps the entity to the account_types table in the finance schema.
        builder.ToTable("account_types", "finance");

        // Configures the primary key.
        builder.HasKey(accountType => accountType.Id).HasName("pk_account_types");

        // Configures the required account type name.
        builder
            .Property(accountType => accountType.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the optional account type description.
        builder
            .Property(accountType => accountType.Description)
            .HasMaxLength(DatabaseLengths.Description);

        // One user can own many account types.
        builder
            .HasOne(accountType => accountType.User)
            .WithMany(user => user.AccountTypes)
            .HasForeignKey(accountType => accountType.UserId)
            .HasConstraintName("fk_account_types_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One account type can be assigned to many accounts.
        builder
            .HasMany(accountType => accountType.Accounts)
            .WithOne(account => account.AccountType)
            .HasForeignKey(account => account.AccountTypeId)
            .HasConstraintName("fk_accounts_account_types_account_type_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Prevents a user from creating duplicate account type names.
        builder
            .HasIndex(accountType => new { accountType.UserId, accountType.Name })
            .IsUnique()
            .HasDatabaseName("ux_account_types_user_id_name");

        // Improves queries that retrieve active account types for a user.
        builder
            .HasIndex(accountType => new { accountType.UserId, accountType.IsActive })
            .HasDatabaseName("ix_account_types_user_id_is_active");
    }
}
