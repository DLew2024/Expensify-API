using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a recurring transaction template.
/// Recurring transactions automatically generate individual transactions
/// based on the configured schedule.
/// </summary>
public class RecurringTransaction : Identifiable
{
    /// <summary>
    /// The user who owns this recurring transaction.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The account that generated transactions will be applied to.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// The budget associated with this recurring transaction.
    /// Null if the transaction is not tied to a budget.
    /// </summary>
    public Guid? BudgetId { get; set; }

    /// <summary>
    /// The category assigned to generated transactions.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// The amount that will be applied each time the transaction recurs.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Indicates whether the recurring transaction is an income, expense, or transfer.
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Description that will be copied to generated transactions.
    /// Example: "Monthly Rent".
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Optional merchant or payee.
    /// Example: Netflix or Employer.
    /// </summary>
    public string? MerchantName { get; set; }

    /// <summary>
    /// Optional notes that will be copied to generated transactions.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// The payment method used by generated transactions.
    /// Null when no payment method is assigned.
    /// </summary>
    public Guid? PaymentMethodId { get; set; }

    /// <summary>
    /// Payment method used by generated transactions.
    /// </summary>
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// How often the transaction repeats.
    /// </summary>
    public RecurringFrequency Frequency { get; set; }

    /// <summary>
    /// The date the recurring schedule begins.
    /// Stored as a long timestamp.
    /// </summary>
    public long StartDate { get; set; }

    /// <summary>
    /// Optional date the recurring schedule ends.
    /// Null indicates the schedule continues indefinitely.
    /// </summary>
    public long? EndDate { get; set; }

    /// <summary>
    /// The next scheduled execution date.
    /// Stored as a long timestamp.
    /// </summary>
    public long NextRunDate { get; set; }

    /// <summary>
    /// Indicates whether the recurring transaction is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for the owning user.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Navigation property for the associated account.
    /// </summary>
    public Account Account { get; set; } = null!;

    /// <summary>
    /// Navigation property for the associated budget.
    /// </summary>
    public Budget? Budget { get; set; }

    /// <summary>
    /// Navigation property for the associated category.
    /// </summary>
    public Category? Category { get; set; }
}
