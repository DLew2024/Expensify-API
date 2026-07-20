using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a type of financial account.
/// Account types may be system-defined or created by a user.
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
    /// Null for system-defined account types.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Navigation property for the user who owns this account type.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Optional description of the account type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this account type is defined and managed by the system.
    /// </summary>
    public bool IsSystemDefault { get; set; }

    /// <summary>
    /// Indicates whether this account type can be assigned to new accounts.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Accounts assigned to this account type.
    /// </summary>
    public List<Account> Accounts { get; set; } = [];
}
