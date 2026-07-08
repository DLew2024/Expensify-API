namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Join model between Budget and Category.
/// Allows one budget to track multiple categories.
/// </summary>
public class BudgetCategory
{
    /// <summary>
    /// Unique identifier for this budget-category relationship.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The budget this category belongs to.
    /// </summary>
    public Guid BudgetId { get; set; }

    /// <summary>
    /// The category included in the budget.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Optional spending limit for this specific category.
    /// Example: Groceries = $500 inside a $2,000 monthly budget.
    /// </summary>
    public decimal? CategoryLimitAmount { get; set; }

    /// <summary>
    /// Budget navigation property.
    /// </summary>
    public Budget Budget { get; set; } = null!;

    /// <summary>
    /// Category navigation property.
    /// </summary>
    public Category Category { get; set; } = null!;
}
