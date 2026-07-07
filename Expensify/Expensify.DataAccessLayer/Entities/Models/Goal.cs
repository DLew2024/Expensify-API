using Expensify.DataAccessLayer.Entities.Enums;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.Models
{
    /// <summary>
    /// Represents a financial goal that a user is working toward.
    /// Goals help users track progress toward savings or debt payoff targets.
    /// </summary>
    public class Goal : IIdentifiable
    {
        /// <summary>
        /// Unique identifier for the goal.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user who owns the goal.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The name of the goal.
        /// Example: "Emergency Fund", "Vacation", "New Car".
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description of the goal.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The target amount the user wants to reach.
        /// </summary>
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// The user's current progress toward the goal.
        /// </summary>
        public decimal CurrentAmount { get; set; }

        /// <summary>
        /// The type of financial goal.
        /// Example: Savings, Debt Payoff, Vacation, or Retirement.
        /// </summary>
        public GoalType Type { get; set; }

        /// <summary>
        /// The desired completion date for the goal.
        /// Null if no deadline has been set.
        /// </summary>
        public long? TargetDate { get; set; }

        /// <summary>
        /// Indicates whether the goal has been completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Indicates whether the goal is currently active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The account associated with this goal.
        /// Null if the goal spans multiple accounts.
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Navigation property for the owning user.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Navigation property for the associated account.
        /// </summary>
        public Account? Account { get; set; }

        /// <summary>
        /// The user who created the goal.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// The date the goal was created.
        /// Stored as a long timestamp.
        /// </summary>
        public long CreateDate { get; set; }

        /// <summary>
        /// The user who last updated the goal.
        /// Null if the goal has never been modified.
        /// </summary>
        public Guid? LastUpdatedBy { get; set; }

        /// <summary>
        /// The date the goal was last updated.
        /// Null if the goal has never been modified.
        /// Stored as a long timestamp.
        /// </summary>
        public long? UpdatedDate { get; set; }
    }
}