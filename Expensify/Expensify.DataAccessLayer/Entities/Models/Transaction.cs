using Expensify.DataAccessLayer.Entities.Enums;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.Models
{
    internal class Transaction : IIdentifiable
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public long TransactionDate { get; set; }
        public TransactionType? Type { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Merchant { get; set; }
        public bool IsRecurring { get; set; }
        public string? Notes { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public TransactionStatus Status { get; set; }
        public Guid? RecurringTransactionId { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }
        public long UpdatedDate { get; set; }
        public long CreateDate { get; set; }
        public List<string> Tags { get; set; } = [];
        //public Guid? AccountId { get; set; }
        //public string? AccountName { get; set; }
        //public decimal AccountBalanceAfterTransaction { get; set; }
        //public Guid? BudgetId { get; set; }
        //public string? BudgetName { get; set; }
    }
}
