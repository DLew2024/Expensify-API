using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

/// <summary>
/// Represents a financial transaction performed by a user.
/// Transactions may represent income, expenses, or transfers between accounts.
/// </summary>
public class Transaction : Identifiable
{
    /// <summary>
    /// The user who owns this transaction.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// The account associated with this transaction.
    /// </summary>
    public required Guid AccountId { get; set; }

    /// <summary>
    /// The budget this transaction contributes to, if applicable.
    /// </summary>
    public Guid? BudgetId { get; set; }

    /// <summary>
    /// The category assigned to this transaction.
    /// Null for transactions that do not require categorization, such as transfers.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// The monetary amount of the transaction.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// The account balance immediately after this transaction was applied.
    /// Used for historical balance tracking.
    /// </summary>
    public required decimal AccountBalanceAfterTransaction { get; set; }

    /// <summary>
    /// Indicates whether the transaction is an income, expense, or transfer.
    /// </summary>
    public required TransactionType Type { get; set; }

    /// <summary>
    /// The current processing status of the transaction.
    /// Example: Pending or Posted.
    /// </summary>
    public required TransactionPostedStatus Status { get; set; }

    /// <summary>
    /// The date the transaction occurred.
    /// Stored as a long timestamp.
    /// </summary>
    public required long TransactionDate { get; set; }

    /// <summary>
    /// Short description of the transaction.
    /// Example: "Monthly Rent" or "Paycheck".
    /// </summary>
    public required string Description { get; set; } = string.Empty;

    /// <summary>
    /// The merchant or payee associated with the transaction.
    /// Example: Walmart, Amazon, Starbucks.
    /// </summary>
    public required string MerchantName { get; set; }

    /// <summary>
    /// Optional notes entered by the user.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Indicates whether this transaction is part of a recurring series.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// The payment method associated with this transaction.
    /// </summary>
    public Guid? PaymentMethodId { get; set; }

    /// <summary>
    /// The payment method used to complete the transaction.
    /// Example: Credit Card, Debit Card, Cash.
    /// </summary>
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Optional tags used to organize or filter transactions.
    /// Example: "Vacation", "Business", "Tax Deductible".
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Navigation property for the transaction owner.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Navigation property for the associated account.
    /// </summary>
    public Account Account { get; set; } = null!;

    /// <summary>
    /// Navigation property for the associated budget.
    /// Null if the transaction is not assigned to a budget.
    /// </summary>
    public Budget? Budget { get; set; }

    /// <summary>
    /// Navigation property for the assigned category.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// The related transaction created as part of the same transfer.
    /// Null if this transaction is not a transfer.
    /// </summary>
    public Guid? LinkedTransactionId { get; set; }

    /// <summary>
    /// Navigation property for the linked transfer transaction.
    /// </summary>
    public Transaction? LinkedTransaction { get; set; }

    public Guid RecurringTransactionId { get; set; }

    public string Icon { get; set; } = string.Empty;
}
