using System.ComponentModel.DataAnnotations;

namespace Expensify.DataAccessLayer.Entities.Models
{
    /// <summary>
    /// Represents a user of the application.
    /// A user can own accounts, transactions, budgets, categories, and recurring transactions.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The user's full name.
        /// </summary>
        [Required]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address.
        /// Used for authentication and account communication.
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's hashed password.
        /// Never store plain text passwords.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// URL of the user's profile image.
        /// </summary>
        public string ProfileImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Financial accounts owned by the user.
        /// </summary>
        public List<Account> Accounts { get; set; } = [];

        /// <summary>
        /// All financial transactions belonging to the user.
        /// Includes income, expenses, and transfers.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = [];

        /// <summary>
        /// Categories created by the user for organizing transactions.
        /// </summary>
        public List<Category> Categories { get; set; } = [];

        /// <summary>
        /// Budgets created and owned by the user.
        /// </summary>
        public List<Budget> OwnedBudgets { get; set; } = [];

        /// <summary>
        /// Budgets that have been shared with the user.
        /// </summary>
        public List<BudgetMember> BudgetMemberships { get; set; } = [];

        /// <summary>
        /// Recurring transaction templates created by the user.
        /// </summary>
        public List<RecurringTransaction> RecurringTransactions { get; set; } = [];
    }
}
