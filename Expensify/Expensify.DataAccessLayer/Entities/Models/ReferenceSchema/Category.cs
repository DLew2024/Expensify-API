using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.DataAccessLayer.Entities.Models.ReferenceSchema;

/// <summary>
/// Represents a category used to organize transactions and budgets.
/// Categories may be system-defined or created by the user.
/// </summary>
public class Category : Auditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    protected Category() { }

    /// <summary>
    /// Initializes a new category with the specified name.
    /// </summary>
    /// <param name="name">The display name of the category.</param>
    public Category(string name)
        : base(name) { }

    /// <summary>
    /// The user who owns this category.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Navigation property for the category owner.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Indicates whether this category is for income or expense transactions.
    /// </summary>
    public CategoryType Type { get; set; }

    /// <summary>
    /// Optional description of the category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this category was provided by the system.
    /// System categories cannot typically be deleted.
    /// </summary>
    public bool IsSystemDefault { get; set; }

    /// <summary>
    /// Indicates whether this category is active.
    /// Inactive categories remain available for historical transactions.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Transactions assigned to this category.
    /// </summary>
    public List<Transaction> Transactions { get; set; } = [];

    /// <summary>
    /// Budget-category relationships that include this category.
    /// </summary>
    public List<BudgetCategory> BudgetCategories { get; set; } = [];
}
