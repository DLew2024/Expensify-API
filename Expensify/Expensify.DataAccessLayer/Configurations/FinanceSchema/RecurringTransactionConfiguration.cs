using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

/// <summary>
/// Configures the database mapping for the <see cref="RecurringTransaction"/> entity.
/// </summary>
public class RecurringTransactionConfiguration : IEntityTypeConfiguration<RecurringTransaction>
{
    /// <summary>
    /// Configures the <see cref="RecurringTransaction"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<RecurringTransaction> builder)
    {
        // Maps the entity to the recurring_transactions table
        // inside the finance schema.
        builder.ToTable("recurring_transactions", "finance");

        // Configures the primary key.
        builder
            .HasKey(recurringTransaction => recurringTransaction.Id)
            .HasName("pk_recurring_transactions");

        // Configures the recurring transaction amount.
        builder
            .Property(recurringTransaction => recurringTransaction.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        // Configures the transaction type enum.
        builder.Property(recurringTransaction => recurringTransaction.Type).IsRequired();

        // Configures the required transaction description.
        builder
            .Property(recurringTransaction => recurringTransaction.Description)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Description);

        // Configures the optional merchant name.
        builder
            .Property(recurringTransaction => recurringTransaction.MerchantName)
            .HasMaxLength(DatabaseLengths.Name);

        // Configures optional notes.
        builder
            .Property(recurringTransaction => recurringTransaction.Notes)
            .HasMaxLength(DatabaseLengths.Notes);

        // Configures the recurrence frequency enum.
        builder.Property(recurringTransaction => recurringTransaction.Frequency).IsRequired();

        // Configures the required schedule start date.
        builder.Property(recurringTransaction => recurringTransaction.StartDate).IsRequired();

        // Configures the optional schedule end date.
        builder.Property(recurringTransaction => recurringTransaction.EndDate).IsRequired(false);

        // Configures the next scheduled execution date.
        builder.Property(recurringTransaction => recurringTransaction.NextRunDate).IsRequired();

        // Configures the active-status flag.
        builder.Property(recurringTransaction => recurringTransaction.IsActive).IsRequired();

        // One user can own many recurring transactions.
        builder
            .HasOne(recurringTransaction => recurringTransaction.User)
            .WithMany(user => user.RecurringTransactions)
            .HasForeignKey(recurringTransaction => recurringTransaction.UserId)
            .HasConstraintName("fk_recurring_transactions_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One account can have many recurring transaction templates.
        // Restrict prevents deleting an account while recurring templates
        // still reference it.
        builder
            .HasOne(recurringTransaction => recurringTransaction.Account)
            .WithMany()
            .HasForeignKey(recurringTransaction => recurringTransaction.AccountId)
            .HasConstraintName("fk_recurring_transactions_accounts_account_id")
            .OnDelete(DeleteBehavior.Restrict);

        // A recurring transaction may optionally belong to a budget.
        // Deleting the budget clears the foreign key without deleting
        // the recurring transaction.
        builder
            .HasOne(recurringTransaction => recurringTransaction.Budget)
            .WithMany()
            .HasForeignKey(recurringTransaction => recurringTransaction.BudgetId)
            .HasConstraintName("fk_recurring_transactions_budgets_budget_id")
            .OnDelete(DeleteBehavior.SetNull);

        // A recurring transaction may optionally use a category.
        // Deleting the category clears the foreign key without deleting
        // the recurring transaction.
        builder
            .HasOne(recurringTransaction => recurringTransaction.Category)
            .WithMany()
            .HasForeignKey(recurringTransaction => recurringTransaction.CategoryId)
            .HasConstraintName("fk_recurring_transactions_categories_category_id")
            .OnDelete(DeleteBehavior.SetNull);

        // A recurring transaction may optionally use a payment method.
        // Deleting the payment method clears the foreign key without deleting
        // the recurring transaction.
        builder
            .HasOne(recurringTransaction => recurringTransaction.PaymentMethod)
            .WithMany()
            .HasForeignKey(recurringTransaction => recurringTransaction.PaymentMethodId)
            .HasConstraintName("fk_recurring_transactions_payment_methods_payment_method_id")
            .OnDelete(DeleteBehavior.SetNull);

        // Improves recurring transaction lookups by user.
        builder
            .HasIndex(recurringTransaction => recurringTransaction.UserId)
            .HasDatabaseName("ix_recurring_transactions_user_id");

        // Improves recurring transaction lookups by account.
        builder
            .HasIndex(recurringTransaction => recurringTransaction.AccountId)
            .HasDatabaseName("ix_recurring_transactions_account_id");

        // Improves scheduling queries that find active recurring
        // transactions that are ready to run.
        builder
            .HasIndex(recurringTransaction => new
            {
                recurringTransaction.IsActive,
                recurringTransaction.NextRunDate,
            })
            .HasDatabaseName("ix_recurring_transactions_is_active_next_run_date");

        // Improves optional filtering by budget.
        builder
            .HasIndex(recurringTransaction => recurringTransaction.BudgetId)
            .HasDatabaseName("ix_recurring_transactions_budget_id");

        // Improves optional filtering by category.
        builder
            .HasIndex(recurringTransaction => recurringTransaction.CategoryId)
            .HasDatabaseName("ix_recurring_transactions_category_id");

        // Improves optional filtering by payment method.
        builder
            .HasIndex(recurringTransaction => recurringTransaction.PaymentMethodId)
            .HasDatabaseName("ix_recurring_transactions_payment_method_id");
    }
}
