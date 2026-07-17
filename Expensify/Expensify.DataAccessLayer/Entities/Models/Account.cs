using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Enums;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a financial account that belongs to a user.
/// An account can contain many transactions and maintains a cached current balance.
/// </summary>
public class Account : Auditable
{
    /// <summary>
    /// The user who owns this account.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user navigation property for the account owner.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// The account type assigned to this account.
    /// </summary>
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// The type of financial account.
    /// Example: Checking, Savings, CreditCard, Cash, Loan.
    /// </summary>
    public AccountType AccountType { get; set; } = null!;

    /// <summary>
    /// The name of the financial institution.
    /// Example: Chase, Fidelity, Capital One.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// The last four digits of the account number for display purposes only.
    /// Example: "4821".
    /// </summary>
    public string? LastFourDigits { get; set; }

    /// <summary>
    /// The currency used by this account.
    /// Example: USD, EUR, GBP.
    /// </summary>
    public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.USD;

    /// <summary>
    /// Cached current balance for dashboard and account list performance.
    /// This should be updated whenever transactions are created, updated, or removed.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// The amount currently available to spend.
    /// This is typically the current balance adjusted for any pending transactions.
    /// For example, a pending debit card purchase may reduce the available balance
    /// before it is reflected in the current balance.
    /// </summary>
    public decimal AvailableBalance { get; set; }

    /// <summary>
    /// Indicates whether this account should be included when calculating net worth.
    /// Example: false for an account the user wants excluded from dashboard totals.
    /// </summary>
    public bool IncludeInNetWorth { get; set; } = true;

    /// <summary>
    /// Indicates whether the account is active.
    /// Inactive accounts keep their history but should not be used for new transactions.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indicates whether the account should be hidden from the main account list.
    /// Hidden accounts still retain their transaction history.
    /// </summary>
    public bool IsHidden { get; set; } = false;

    /// <summary>
    /// Optional notes about the account.
    /// Example: "Used only for travel expenses."
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Maximum borrowable amount for credit-based accounts.
    /// Applies mainly to credit cards and lines of credit.
    /// </summary>
    public decimal? CreditLimit { get; set; }

    /// <summary>
    /// Annual interest rate or APR for the account.
    /// Applies mainly to savings, loans, credit cards, and investment-like accounts.
    /// </summary>
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Date the account was closed.
    /// Null means the account is still open.
    /// </summary>
    public long? ClosedDate { get; set; }

    /// <summary>
    /// All transactions associated with this account.
    /// </summary>
    public List<Transaction> Transactions { get; set; } = [];
}
