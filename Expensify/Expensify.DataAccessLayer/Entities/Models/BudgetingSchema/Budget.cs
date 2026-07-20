using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;

/// <summary>
/// Represents a budget created by a user.
/// A budget can track spending limits across one or more categories and can be shared with other users.
/// </summary>
public class Budget : Auditable
{
    /// <summary>
    /// The user who created/owns the budget.
    /// </summary>
    public Guid OwnerUserId { get; set; }

    /// <summary>
    /// Optional description for the budget.
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Total spending limit for this budget.
    /// </summary>
    public decimal LimitAmount { get; set; } = 0;

    /// <summary>
    /// How often the budget resets.
    /// Example: Weekly, Monthly, Yearly.
    /// </summary>
    public BudgetPeriod Period { get; set; } = BudgetPeriod.Monthly;

    /// <summary>
    /// The date the budget starts.
    /// </summary>
    public long StartDate { get; set; } = 0;

    /// <summary>
    /// Optional date the budget ends.
    /// Null means the budget continues indefinitely.
    /// </summary>
    public long? EndDate { get; set; }

    /// <summary>
    /// Indicates whether the budget is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indicates whether this budget is shared with other users.
    /// </summary>
    public bool IsShared { get; set; } = false;

    /// <summary>
    /// The owner navigation property.
    /// </summary>
    public User OwnerUser { get; set; } = null!;

    /// <summary>
    /// Categories included in this budget.
    /// </summary>
    public List<BudgetCategory> BudgetCategories { get; set; } = [];

    /// <summary>
    /// Users who have access to this budget.
    /// </summary>
    public List<BudgetMember> Members { get; set; } = [];
}
