using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.DashboardDTOs;

public class DashboardDataResponseDTO
{
    public decimal TotalBalance { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public TransactionPeriodSummaryDTO? Last30DaysOfExpenses { get; set; }
    public TransactionPeriodSummaryDTO? Last60DaysOfExpenses { get; set; }
    public TransactionPeriodSummaryDTO? Last30DaysOfIncome { get; set; }
    public TransactionPeriodSummaryDTO? Last60DaysOfIncome { get; set; }
    public TransactionDTO[] RecentTransactions { get; set; } = [];
}

public class TransactionPeriodSummaryDTO
{
    public decimal TotalBalance { get; set; }
    public TransactionDTO[] Transactions { get; set; } = [];
}

public class TransactionDTO
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public long TransactionDate { get; set; }
    public string Merchant { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public required PaymentMethodDTO PaymentMethod { get; set; }
    public TransactionType Type { get; set; }
    public TransactionPostedStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsRecurring { get; set; }
    public Guid RecurringTransactionId { get; set; }
    public List<string> Tags { get; set; } = [];

    public static readonly Expression<Func<Transaction, TransactionDTO>> Projection =
        transaction => new TransactionDTO
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            TransactionDate = transaction.TransactionDate,
            Merchant = transaction.MerchantName,
            Description = transaction.Description,
            Notes = transaction.Notes ?? string.Empty,
            PaymentMethod =
                transaction.PaymentMethod != null
                    ? new PaymentMethodDTO
                    {
                        Id = transaction.PaymentMethod.Id,
                        Name = transaction.PaymentMethod.Name,
                    }
                    : new PaymentMethodDTO { Id = Guid.Empty, Name = string.Empty },
            Type = transaction.Type,
            Status = transaction.Status,
            CategoryId = transaction.CategoryId ?? Guid.Empty,
            CategoryName = transaction.Category != null ? transaction.Category.Name : string.Empty,
            IsRecurring = transaction.IsRecurring,
            Tags = transaction.Tags,
        };
}

public class PaymentMethodDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
