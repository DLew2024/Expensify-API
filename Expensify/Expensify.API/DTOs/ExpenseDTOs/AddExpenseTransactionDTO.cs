using System.ComponentModel.DataAnnotations;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.ExpenseDTOs;

public class AddExpenseTransactionDTO
{
    /// <summary>
    /// The account that will receive the expense.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount of expense received.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// The date the expense occurred.
    /// </summary>
    [Required]
    public long TransactionDate { get; set; }

    /// <summary>
    /// A description of the expense transaction.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The employer, payer, or other source of the expense.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Optional notes entered by the user.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Indicates whether the expense belongs to a recurring series.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Optional payment method used to transaction the expense.
    /// </summary>
    public Guid? PaymentMethodId { get; set; }

    /// <summary>
    /// Optional tags used to organize the expense transaction.
    /// </summary>
    public List<string> Tags { get; set; } = [];

    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Optional budget to associate with the expnse.
    /// </summary>
    public Guid? BudgetId { get; set; }

    /// <summary>
    /// Optional expense category, such as Rent, Travel, or Interest.
    /// </summary>
    public Guid? CategoryId { get; set; }

    public Transaction ToTransaction(Guid userId, decimal accountBalanceAfterTransaction)
    {
        return new Transaction
        {
            UserId = userId,
            AccountId = AccountId,
            Amount = Amount,
            AccountBalanceAfterTransaction = accountBalanceAfterTransaction,
            Type = TransactionType.Expense,
            Status = TransactionPostedStatus.Posted,
            TransactionDate = TransactionDate,
            Description = Description.Trim(),
            MerchantName = Source.Trim(),
            Notes = Notes?.Trim(),
            IsRecurring = IsRecurring,
            PaymentMethodId = PaymentMethodId,
            Tags = Tags,
            Icon = Icon.Trim(),
            BudgetId = BudgetId,
            CategoryId = CategoryId,
            CreatedBy = userId,
            LastUpdatedBy = userId,
        };
    }
}
