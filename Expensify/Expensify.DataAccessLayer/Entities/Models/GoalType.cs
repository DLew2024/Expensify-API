using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a type of financial goal.
/// Goal types may be system-defined or created by the user.
/// </summary>
public class GoalType : Auditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GoalType"/> class.
    /// Required by Entity Framework.
    /// </summary>
    protected GoalType()
    {
    }

    /// <summary>
    /// Initializes a new goal type with the specified name.
    /// </summary>
    /// <param name="name">The display name of the goal type.</param>
    public GoalType(string name)
        : base(name)
    {
    }

    /// <summary>
    /// The user who owns this goal type.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Optional description of the goal type.
    /// Example: "Vacation", "Emergency Fund", or "Retirement".
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this goal type was provided by the system.
    /// System goal types cannot typically be deleted.
    /// </summary>
    public bool IsSystemDefault { get; set; }

    /// <summary>
    /// Indicates whether this goal type is active.
    /// Inactive goal types cannot be assigned to new goals.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for the user who owns this goal type.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Goals assigned to this goal type.
    /// </summary>
    public List<Goal> Goals { get; set; } = [];
}