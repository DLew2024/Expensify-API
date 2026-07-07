using Expensify.DataAccessLayer.Entities.Enums;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.Models
{
    /// <summary>
    /// Represents a category used to organize transactions.
    /// Categories can be used for budgeting, reporting, and transaction classification.
    /// </summary>
    public class Category : IAuditableEntity
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user who owns this category.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The category name.
        /// Example: Groceries, Rent, Utilities, Salary.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether this category is used for income or expense transactions.
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Optional description of the category.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether this is a system-defined category.
        /// System categories cannot be deleted by users.
        /// </summary>
        public bool IsSystemCategory { get; set; }

        /// <summary>
        /// Indicates whether this category is active.
        /// Inactive categories remain available for historical transactions.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Icon identifier used when displaying the category.
        /// Example: "shopping-cart", "house", "car".
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Accent color used for displaying the category.
        /// Example: "#16A34A".
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Navigation property for the category owner.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Transactions assigned to this category.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = [];

        /// <summary>
        /// Budgets that include this category.
        /// </summary>
        /// <summary>
        /// The user who created the category.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Date the category was created.
        /// Stored as a long timestamp.
        /// </summary>
        public long CreateDate { get; set; }

        /// <summary>
        /// Date the category was last updated.
        /// Null if the category has never been updated.
        /// </summary>
        public long? UpdatedDate { get; set; }

        /// <summary>
        /// The user who last updated the category.
        /// Null if the category has never been updated.
        /// </summary>
        public Guid? LastUpdatedBy { get; set; }
    }
}
