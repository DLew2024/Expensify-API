using System.ComponentModel.DataAnnotations;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.IncomeDTOs;

public class AddIncomeTransactionDTO
{
    /// <summary>
    /// The account that will receive the income.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount of income received.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// The date the income was received.
    /// </summary>
    [Required]
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
    /// Payment method used to receive the income.
    /// </summary>
    public required Guid PaymentMethodId { get; set; }

    /// <summary>
    /// The URL to the emoji icon
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Optional notes entered by the user.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Optional tags used to organize the income transaction.
    /// </summary>
    //public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Optional budget to associate with the income.
    /// </summary>
    //public Guid? BudgetId { get; set; }

    /// <summary>
    /// Optional income category, such as Salary, Bonus, or Interest.
    /// </summary>
    //public Guid? CategoryId { get; set; }

    /// <summary>
    /// Indicates whether the income belongs to a recurring series.
    /// </summary>
    //public bool IsRecurring { get; set; }

    public Transaction ToTransaction(Guid userId, decimal accountBalanceAfterTransaction)
    {
        return new Transaction
        {
            UserId = userId,
            AccountId = AccountId,
            Amount = Amount,
            AccountBalanceAfterTransaction = accountBalanceAfterTransaction,
            MerchantName = Source,
            Type = TransactionType.Income,
            Status = TransactionPostedStatus.Posted,
            TransactionDate = TransactionDate,
            Description = Description,
            Icon = Icon,
            Notes = Notes,
            CreatedBy = userId,
            LastUpdatedBy = userId,
            PaymentMethodId = PaymentMethodId,
            //IsRecurring = IsRecurring,
            //CategoryId = CategoryId,
            //BudgetId = BudgetId,
            //Tags = Tags,
        };
    }
}
