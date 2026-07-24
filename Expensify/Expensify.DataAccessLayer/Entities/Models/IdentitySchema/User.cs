using Expensify.DataAccessLayer.Entities.Models.BudgetingSchema;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

/// <summary>
/// Represents a user of the application.
/// A user can own accounts, transactions, budgets, categories, and recurring transactions.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The user's full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address.
    /// Used for authentication and account communication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's hashed password.
    /// Never store plain text passwords.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// URL of the user's profile image.
    /// </summary>
    public string ProfileImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the role assigned to the user.
    /// </summary>
    public Guid RoleId { get; set; } = RoleIds.User;

    /// <summary>
    /// The role assigned to the user.
    /// </summary>
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Indicates whether the user's email address has been verified.
    /// </summary>
    public bool IsEmailVerified { get; set; }

    /// <summary>
    /// The date the user's email address was verified.
    /// Null if the email has not been verified.
    /// Stored as a Unix timestamp.
    /// </summary>
    public long? EmailVerifiedAt { get; set; }

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

    /// <summary>
    /// Password reset tokens issued for this user.
    /// </summary>
    public List<PasswordResetToken> PasswordResetTokens { get; set; } = [];

    /// <summary>
    /// Email-verification tokens issued for this user.
    /// </summary>
    public List<EmailVerificationToken> EmailVerificationTokens { get; set; } = [];

    /// <summary>
    /// Refresh tokens issued to this user.
    /// </summary>
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    /// <summary>
    /// Account types created by or assigned to this user.
    /// </summary>
    public List<AccountType> AccountTypes { get; set; } = [];

    /// <summary>
    /// Payment methods created by the user.
    /// </summary>
    public List<PaymentMethod> PaymentMethods { get; set; } = [];
}
