using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.DashboardDTOs
{
    public class DashboardDataResponseDTO
    {
        public int TotalBalance { get; set; }
        public int TotalIncome { get; set; }
        public int TotalExpenses { get; set; }
        public DaysOfExpensesDTO? Last30DaysOfExpenses { get; set; } 
        public DaysOfExpensesDTO? Last60DaysOfExpenses { get; set; } 
        public TransactionDTO[] RecentTransactions { get; set; } = [];
    }

    public class DaysOfExpensesDTO
    {
        public int TotalBalance { get; set; }
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
        public PaymentMethod PaymentMethod { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsRecurring { get; set; }
        public Guid RecurringTransactionId { get; set; }
        public List<string> Tags { get; set; } = [];
    }
}
