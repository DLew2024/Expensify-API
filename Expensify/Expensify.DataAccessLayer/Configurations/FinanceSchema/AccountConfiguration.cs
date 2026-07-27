using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

/// <summary>
/// Configures the database mapping, relationships, constraints,
/// precision rules, and indexes for the <see cref="Account"/> entity.
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
        // Maps the Account entity to the accounts table in the finance schema.
        builder.ToTable("accounts", "finance");

        // Configures the primary key for the accounts table.
        builder.HasKey(account => account.Id).HasName("pk_accounts");

        // Configures the account name as required with a maximum allowed length.
        builder.Property(account => account.Name).IsRequired().HasMaxLength(DatabaseLengths.Name);

        // Configures the financial institution name as required.
        builder
            .Property(account => account.InstitutionName)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.InstitutionName);

        // Configures the last four digits of the account number as required.
        builder
            .Property(account => account.LastFourDigits)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.LastFourDigits);

        // Configures optional notes associated with the account.
        builder.Property(account => account.Notes).HasMaxLength(DatabaseLengths.Notes);

        // Configures the optional icon URL associated with the account.
        builder.Property(account => account.Icon).HasMaxLength(DatabaseLengths.Url);

        // Configures the current account balance with fixed monetary precision.
        builder.Property(account => account.CurrentBalance).IsRequired().HasPrecision(18, 2);

        // Configures the available account balance with fixed monetary precision.
        builder.Property(account => account.AvailableBalance).IsRequired().HasPrecision(18, 2);

        // Configures the optional credit limit with fixed monetary precision.
        builder.Property(account => account.CreditLimit).HasPrecision(18, 2);

        // Configures the optional interest rate.
        // Supports percentage values such as 12.75%.
        builder.Property(account => account.InterestRate).HasPrecision(5, 2);

        // Configures the relationship between accounts and users.
        // One user can own many accounts.
        builder
            .HasOne(account => account.User)
            .WithMany(user => user.Accounts)
            .HasForeignKey(account => account.UserId)
            .HasConstraintName("fk_accounts_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Configures the relationship between accounts and currency codes.
        // Each account must reference one currency from the seeded reference data.
        // Currency records cannot be deleted while accounts still reference them.
        builder
            .HasOne(account => account.CurrencyCode)
            .WithMany()
            .HasForeignKey(account => account.CurrencyCodeId)
            .HasConstraintName("fk_accounts_currency_codes_currency_code_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Configures the relationship between accounts and account types.
        // One account type can be assigned to many accounts.
        // Account types cannot be deleted while accounts still reference them.
        builder
            .HasOne(account => account.AccountType)
            .WithMany(accountType => accountType.Accounts)
            .HasForeignKey(account => account.AccountTypeId)
            .HasConstraintName("fk_accounts_account_types_account_type_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Configures the relationship between accounts and transactions.
        // One account can contain many transactions.
        //
        // Restricts physical deletion of an account while transactions still
        // reference it, preserving historical financial records.
        // Accounts should be soft-deleted using IsDeleted instead.
        builder
            .HasMany(account => account.Transactions)
            .WithOne(transaction => transaction.Account)
            .HasForeignKey(transaction => transaction.AccountId)
            .HasConstraintName("fk_transactions_accounts_account_id")
            .OnDelete(DeleteBehavior.Restrict);

        // Improves queries that retrieve active accounts for a specific user.
        builder
            .HasIndex(account => new { account.UserId, account.IsActive })
            .HasDatabaseName("ix_accounts_user_id_is_active");

        // Improves queries that retrieve hidden or visible accounts for a user.
        builder
            .HasIndex(account => new { account.UserId, account.IsHidden })
            .HasDatabaseName("ix_accounts_user_id_is_hidden");

        // Improves account lookups and filtering by account type.
        builder
            .HasIndex(account => account.AccountTypeId)
            .HasDatabaseName("ix_accounts_account_type_id");

        // Improves account lookups and filtering by currency.
        builder
            .HasIndex(account => account.CurrencyCodeId)
            .HasDatabaseName("ix_accounts_currency_code_id");

        // Configures whether the account is the user's default account.
        // Defaults to false when no value is explicitly provided.
        builder.Property(account => account.IsDefault).IsRequired().HasDefaultValue(false);

        // Ensures that each user can have only one default account.
        // The partial unique index applies only to rows where IsDefault is true.
        builder
            .HasIndex(account => account.UserId)
            .IsUnique()
            .HasFilter("\"is_default\" = true")
            .HasDatabaseName("ux_accounts_user_default");
    }
}
