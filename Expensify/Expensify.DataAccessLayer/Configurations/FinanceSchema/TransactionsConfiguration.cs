using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

/// <summary>
/// Configures the database mapping for the <see cref="Transaction"/> entity.
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    /// <summary>
    /// Configures the <see cref="Transaction"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        // Maps the entity to the transactions table in the finance schema.
        builder.ToTable("transactions", "finance");

        // Configures the primary key.
        builder.HasKey(transaction => transaction.Id).HasName("pk_transactions");

        // Configures monetary precision.
        builder.Property(transaction => transaction.Amount).IsRequired().HasPrecision(18, 2);

        builder
            .Property(transaction => transaction.AccountBalanceAfterTransaction)
            .IsRequired()
            .HasPrecision(18, 2);

        // Configures transaction enums.
        builder.Property(transaction => transaction.Type).IsRequired();

        builder.Property(transaction => transaction.Status).IsRequired();

        // Configures the transaction date.
        builder.Property(transaction => transaction.TransactionDate).IsRequired();

        // Configures required and optional text fields.
        builder
            .Property(transaction => transaction.Description)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Description);

        builder
            .Property(transaction => transaction.MerchantName)
            .HasMaxLength(DatabaseLengths.Name);

        builder.Property(transaction => transaction.Notes).HasMaxLength(DatabaseLengths.Notes);

        // Stores tags as a PostgreSQL text array.
        builder.Property(transaction => transaction.Tags).IsRequired().HasColumnType("text[]");

        // One user can own many transactions.
        builder
            .HasOne(transaction => transaction.User)
            .WithMany(user => user.Transactions)
            .HasForeignKey(transaction => transaction.UserId)
            .HasConstraintName("fk_transactions_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One account can contain many transactions.
        // Restrict prevents an account deletion from removing financial history.
        builder
            .HasOne(transaction => transaction.Account)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountId)
            .HasConstraintName("fk_transactions_accounts_account_id")
            .OnDelete(DeleteBehavior.Restrict);

        // A transaction may optionally belong to a budget.
        builder
            .HasOne(transaction => transaction.Budget)
            .WithMany()
            .HasForeignKey(transaction => transaction.BudgetId)
            .HasConstraintName("fk_transactions_budgets_budget_id")
            .OnDelete(DeleteBehavior.SetNull);

        // A transaction may optionally belong to a category.
        builder
            .HasOne(transaction => transaction.Category)
            .WithMany(category => category.Transactions)
            .HasForeignKey(transaction => transaction.CategoryId)
            .HasConstraintName("fk_transactions_categories_category_id")
            .OnDelete(DeleteBehavior.SetNull);

        // A transaction may optionally use a payment method.
        builder
            .HasOne(transaction => transaction.PaymentMethod)
            .WithMany(paymentMethod => paymentMethod.Transactions)
            .HasForeignKey(transaction => transaction.PaymentMethodId)
            .HasConstraintName("fk_transactions_payment_methods_payment_method_id")
            .OnDelete(DeleteBehavior.SetNull);

        // Configures the optional one-to-one linked transaction relationship.
        builder
            .HasOne(transaction => transaction.LinkedTransaction)
            .WithOne()
            .HasForeignKey<Transaction>(transaction => transaction.LinkedTransactionId)
            .HasConstraintName("fk_transactions_transactions_linked_transaction_id")
            .OnDelete(DeleteBehavior.SetNull);

        // Improves date-based account transaction queries.
        builder
            .HasIndex(transaction => new { transaction.AccountId, transaction.TransactionDate })
            .HasDatabaseName("ix_transactions_account_id_transaction_date");

        // Improves transaction history and sorting queries by user.
        builder
            .HasIndex(transaction => new { transaction.UserId, transaction.TransactionDate })
            .HasDatabaseName("ix_transactions_user_id_transaction_date");

        // Improves status-based transaction queries.
        builder
            .HasIndex(transaction => new { transaction.UserId, transaction.Status })
            .HasDatabaseName("ix_transactions_user_id_status");

        // Improves optional budget filtering.
        builder
            .HasIndex(transaction => transaction.BudgetId)
            .HasDatabaseName("ix_transactions_budget_id");

        // Improves optional category filtering.
        builder
            .HasIndex(transaction => transaction.CategoryId)
            .HasDatabaseName("ix_transactions_category_id");

        // Improves optional payment-method filtering.
        builder
            .HasIndex(transaction => transaction.PaymentMethodId)
            .HasDatabaseName("ix_transactions_payment_method_id");

        // Ensures a transaction can only be linked from one other transaction.
        builder
            .HasIndex(transaction => transaction.LinkedTransactionId)
            .IsUnique()
            .HasFilter("\"LinkedTransactionId\" IS NOT NULL")
            .HasDatabaseName("ux_transactions_linked_transaction_id");
    }
}
