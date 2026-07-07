using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a type of financial account.
/// Account types may be system-defined or created by the user.
/// </summary>
public class AccountType : Auditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountType"/> class.
    /// Required by Entity Framework.
    /// </summary>
    protected AccountType() { }

    /// <summary>
    /// Initializes a new account type with the specified name.
    /// </summary>
    /// <param name="name">The display name of the account type.</param>
    public AccountType(string name)
        : base(name) { }

    /// <summary>
    /// The user who owns this account type.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Optional description of the account type.
    /// Example: "Primary checking account" or "High-yield savings account".
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this account type was provided by the system.
    /// System account types cannot typically be deleted.
    /// </summary>
    public bool IsSystemDefault { get; set; }

    /// <summary>
    /// Indicates whether this account type is active.
    /// Inactive account types cannot be assigned to new accounts.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Icon identifier used when displaying the account type.
    /// Example: "bank", "credit-card", or "wallet".
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Accent color used when displaying the account type.
    /// Example: "#2563EB".
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Navigation property for the user who owns this account type.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Accounts that use this account type.
    /// </summary>
    public List<Account> Accounts { get; set; } = [];
}
