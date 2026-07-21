using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.DashboardDTOs;

public class DashboardDataResponseDTO
{
    [Required]
    public decimal TotalBalance { get; set; }

    [Required]
    public decimal TotalIncome { get; set; }

    [Required]
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
    [Required]
    public Guid Id { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public long TransactionDate { get; set; }
    public string Merchant { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public required PaymentMethodDTO PaymentMethod { get; set; }
    public required CategoryDTO Category { get; set; }
    public TransactionPostedStatus Status { get; set; }
    public bool IsRecurring { get; set; }
    public Guid RecurringTransactionId { get; set; }
    public List<string> Tags { get; set; } = [];
    public string Icon { get; set; } = string.Empty;

    public static readonly Expression<Func<Transaction, TransactionDTO>> Projection =
        transaction => new TransactionDTO
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Type = transaction.Type,
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

            Category =
                transaction.Category != null
                    ? new CategoryDTO
                    {
                        Id = transaction.Category.Id,
                        Name = transaction.Category.Name,
                    }
                    : new CategoryDTO { Id = Guid.Empty, Name = string.Empty },

            Status = transaction.Status,
            IsRecurring = transaction.IsRecurring,
            RecurringTransactionId = transaction.RecurringTransactionId,
            Tags = transaction.Tags,
            Icon = transaction.Icon,
        };
}

public class PaymentMethodDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class CategoryDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
