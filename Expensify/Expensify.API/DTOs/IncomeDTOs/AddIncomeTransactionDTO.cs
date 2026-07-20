using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.IncomeDTOs;

public class AddIncomeTransactionDTO
{
    /// <summary>
    /// The account that will receive the income.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Optional budget to associate with the income.
    /// </summary>
    public Guid? BudgetId { get; set; }

    /// <summary>
    /// Optional income category, such as Salary, Bonus, or Interest.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// The amount of income received.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The date the income was received.
    /// </summary>
    public long TransactionDate { get; set; }

    /// <summary>
    /// A description of the income transaction.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The employer, payer, or other source of the income.
    /// </summary>
    public required string Source { get; set; }

    /// <summary>
    /// Optional notes entered by the user.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Indicates whether the income belongs to a recurring series.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Optional payment method used to receive the income.
    /// </summary>
    public Guid? PaymentMethodId { get; set; }

    /// <summary>
    /// Optional tags used to organize the income transaction.
    /// </summary>
    public List<string> Tags { get; set; } = [];

    public string Icon { get; set; } = string.Empty;

    public Transaction ToTransaction(Guid userId, decimal accountBalanceAfterTransaction)
    {
        return new Transaction
        {
            UserId = userId,
            AccountId = AccountId,
            BudgetId = BudgetId,
            CategoryId = CategoryId,
            Amount = Amount,
            AccountBalanceAfterTransaction = accountBalanceAfterTransaction,
            Type = TransactionType.Income,
            Status = TransactionPostedStatus.Posted,
            TransactionDate = TransactionDate,
            Description = Description,
            MerchantName = Source,
            Notes = Notes,
            IsRecurring = IsRecurring,
            PaymentMethodId = PaymentMethodId,
            Tags = Tags,
            CreatedBy = userId,
            LastUpdatedBy = userId,
        };
    }
}
