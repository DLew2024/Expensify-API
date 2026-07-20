using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;

/// <summary>
/// Represents a user's access to a shared budget.
/// </summary>
public class BudgetMember : Auditable
{
    /// <summary>
    /// The budget the user has access to.
    /// </summary>
    public Guid BudgetId { get; set; }

    /// <summary>
    /// The user who has access to the budget.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user's permission level within the budget.
    /// </summary>
    public BudgetMemberRole Role { get; set; }

    /// <summary>
    /// Date the user was added to the budget.
    /// Stored as a long timestamp.
    /// </summary>
    public long JoinedDate { get; set; }

    /// <summary>
    /// Indicates whether the member is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Budget navigation property.
    /// </summary>
    public Budget Budget { get; set; } = null!;

    /// <summary>
    /// User navigation property.
    /// </summary>
    public User User { get; set; } = null!;
}
