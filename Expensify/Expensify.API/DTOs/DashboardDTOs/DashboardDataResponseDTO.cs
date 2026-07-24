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

    public AccountSummaryDTO? Account { get; set; }

    public TransactionPeriodSummaryDTO? Last30DaysOfExpenses { get; set; }

    public TransactionPeriodSummaryDTO? Last60DaysOfExpenses { get; set; }

    public TransactionPeriodSummaryDTO? Last30DaysOfIncome { get; set; }

    public TransactionPeriodSummaryDTO? Last60DaysOfIncome { get; set; }

    public TransactionDTO[] RecentTransactions { get; set; } = [];

    public static DashboardDataResponseDTO Create(
        AccountSummaryDTO account,
        TransactionDTO[] transactions
    )
    {
        var currentDate = DateTimeOffset.UtcNow;

        var thirtyDaysAgo = currentDate.AddDays(-30).ToUnixTimeSeconds();

        var sixtyDaysAgo = currentDate.AddDays(-60).ToUnixTimeSeconds();

        var income = transactions
            .Where(transaction => transaction.Type == TransactionType.Income)
            .ToArray();

        var expenses = transactions
            .Where(transaction => transaction.Type == TransactionType.Expense)
            .ToArray();

        var last30DaysOfExpenses = expenses
            .Where(transaction => transaction.TransactionDate >= thirtyDaysAgo)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToArray();

        var last60DaysOfExpenses = expenses
            .Where(transaction => transaction.TransactionDate >= sixtyDaysAgo)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToArray();

        var last30DaysOfIncome = income
            .Where(transaction => transaction.TransactionDate >= thirtyDaysAgo)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToArray();

        var last60DaysOfIncome = income
            .Where(transaction => transaction.TransactionDate >= sixtyDaysAgo)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToArray();

        var totalIncome = income.Sum(transaction => transaction.Amount);

        var totalExpenses = expenses.Sum(transaction => transaction.Amount);

        return new DashboardDataResponseDTO
        {
            Account = account,

            TotalBalance = account.CurrentBalance + totalIncome - totalExpenses,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,

            Last30DaysOfExpenses = CreateTransactionPeriodSummary(last30DaysOfExpenses),

            Last60DaysOfExpenses = CreateTransactionPeriodSummary(last60DaysOfExpenses),

            Last30DaysOfIncome = CreateTransactionPeriodSummary(last30DaysOfIncome),

            Last60DaysOfIncome = CreateTransactionPeriodSummary(last60DaysOfIncome),

            RecentTransactions = transactions
                .OrderByDescending(transaction => transaction.TransactionDate)
                .Take(5)
                .ToArray(),
        };
    }

    private static TransactionPeriodSummaryDTO CreateTransactionPeriodSummary(
        TransactionDTO[] transactions
    )
    {
        return new TransactionPeriodSummaryDTO
        {
            TotalBalance = transactions.Sum(transaction => transaction.Amount),
            Transactions = transactions,
        };
    }
}

/// <summary>
/// Represents the user's default account information.
/// </summary>
public class AccountSummaryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
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

    [Required]
    public string Merchant { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public TransactionPostedStatus Status { get; set; }

    //public List<string> Tags { get; set; } = [];
    //public Guid RecurringTransactionId { get; set; }
    // Not implmented yet
    //public required CategoryDTO Category { get; set; }
    //public required PaymentMethodDTO PaymentMethod { get; set; }
    //public bool IsRecurring { get; set; }

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
            Status = transaction.Status,
            Icon = transaction.Icon,

            // Not implmented yet
            //Tags = transaction.Tags,
            //RecurringTransactionId = transaction.RecurringTransactionId,
            //IsRecurring = transaction.IsRecurring,
            //PaymentMethod =
            //    transaction.PaymentMethod != null
            //        ? new PaymentMethodDTO
            //        {
            //            Id = transaction.PaymentMethod.Id,
            //            Name = transaction.PaymentMethod.Name,
            //        }
            //        : new PaymentMethodDTO { Id = Guid.Empty, Name = string.Empty },
            //Category =
            //    transaction.Category != null
            //        ? new CategoryDTO
            //        {
            //            Id = transaction.Category.Id,
            //            Name = transaction.Category.Name,
            //        }
            //        : new CategoryDTO { Id = Guid.Empty, Name = string.Empty },
        };
}
