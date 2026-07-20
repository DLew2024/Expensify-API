using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.IncomeDTOs;

/// <summary>
/// Represents the newly created income transaction.
/// </summary>
public class IncomeTransactionResponseDTO
{
    /// <summary>
    /// The unique identifier of the transaction.
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// The account the income was deposited into.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount of income received.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The account balance immediately after the transaction.
    /// </summary>
    public decimal AccountBalanceAfterTransaction { get; set; }

    /// <summary>
    /// The date the income transaction occurred.
    /// </summary>
    public long TransactionDate { get; set; }

    /// <summary>
    /// The description of the transaction.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The employer or payer associated with the income.
    /// </summary>
    public string MerchantName { get; set; } = string.Empty;

    /// <summary>
    /// Optional notes for the transaction.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Indicates whether the transaction is recurring.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// The status of the transaction.
    /// </summary>
    public TransactionPostedStatus Status { get; set; }

    /// <summary>
    /// Optional tags assigned to the transaction.
    /// </summary>
    public List<string> Tags { get; set; } = [];

    public static IncomeTransactionResponseDTO FromTransaction(Transaction transaction)
    {
        return new IncomeTransactionResponseDTO
        {
            TransactionId = transaction.Id,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            AccountBalanceAfterTransaction = transaction.AccountBalanceAfterTransaction,
            TransactionDate = transaction.TransactionDate,
            Description = transaction.Description,
            MerchantName = transaction.MerchantName,
            Notes = transaction.Notes,
            IsRecurring = transaction.IsRecurring,
            Status = transaction.Status,
            Tags = [.. transaction.Tags],
        };
    }
}
