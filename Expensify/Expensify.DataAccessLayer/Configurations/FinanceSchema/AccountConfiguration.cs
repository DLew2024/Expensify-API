using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

/// <summary>
/// Configures the database mapping for the <see cref="Account"/> entity.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    /// <summary>
    /// Configures the <see cref="Account"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Maps accounts to the finance schema.
        builder.ToTable("accounts", "finance");

        // Configures the primary key.
        builder.HasKey(account => account.Id)
            .HasName("pk_accounts");

        // Configures required string properties.
        builder.Property(account => account.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures optional string properties.
        builder.Property(account => account.InstitutionName)
            .HasMaxLength(DatabaseLengths.InstitutionName);

        builder.Property(account => account.LastFourDigits)
            .HasMaxLength(DatabaseLengths.LastFourDigits);

        builder.Property(account => account.Notes)
            .HasMaxLength(DatabaseLengths.Notes);

        // Stores CurrencyCode as an integer by default.
        builder.Property(account => account.CurrencyCode)
            .IsRequired();

        // Configures monetary precision.
        builder.Property(account => account.CurrentBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(account => account.AvailableBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(account => account.CreditLimit)
            .HasPrecision(18, 2);

        // Supports percentage values such as 12.75.
        builder.Property(account => account.InterestRate)
            .HasPrecision(5, 2);

        // One user can own many accounts.
        builder.HasOne(account => account.User)
            .WithMany(user => user.Accounts)
            .HasForeignKey(account => account.UserId)
            .HasConstraintName("fk_accounts_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One account type can be assigned to many accounts.
        builder.HasOne(account => account.AccountType)
            .WithMany(accountType => accountType.Accounts)
            .HasForeignKey(account => account.AccountTypeId)
            .HasConstraintName("fk_accounts_account_types_account_type_id")
            .OnDelete(DeleteBehavior.Restrict);

        // One account can contain many transactions.
        builder.HasMany(account => account.Transactions)
            .WithOne(transaction => transaction.Account)
            .HasForeignKey(transaction => transaction.AccountId)
            .HasConstraintName("fk_transactions_accounts_account_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Improves filtering active accounts for a user.
        builder.HasIndex(account => new
        {
            account.UserId,
            account.IsActive
        })
        .HasDatabaseName("ix_accounts_user_id_is_active");

        // Improves filtering hidden accounts for a user.
        builder.HasIndex(account => new
        {
            account.UserId,
            account.IsHidden
        })
        .HasDatabaseName("ix_accounts_user_id_is_hidden");

        // Improves account lookups by account type.
        builder.HasIndex(account => account.AccountTypeId)
            .HasDatabaseName("ix_accounts_account_type_id");
    }
}