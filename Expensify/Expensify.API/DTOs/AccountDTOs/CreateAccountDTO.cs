using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.AccountDTOs;

/// <summary>
/// Represents a request to create a new financial account.
/// </summary>
public class CreateAccountDTO
{
    /// <summary>
    /// The user-defined name of the account.
    /// Example: "Main Checking", "Emergency Savings", or "Capital One Credit Card".
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the account type.
    /// </summary>
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// The name of the financial institution.
    /// Example: Chase, Fidelity, or Capital One.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// The last four digits of the account number for display purposes.
    /// </summary>
    public string? LastFourDigits { get; set; }

    /// <summary>
    /// The currency used by the account.
    /// </summary>
    public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.USD;

    /// <summary>
    /// The initial balance of the account.
    /// </summary>
    public decimal InitialBalance { get; set; }

    /// <summary>
    /// Indicates whether the account should be included when calculating net worth.
    /// </summary>
    public bool IncludeInNetWorth { get; set; } = true;

    /// <summary>
    /// Optional notes associated with the account.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Maximum borrowable amount for credit-based accounts.
    /// </summary>
    public decimal? CreditLimit { get; set; }

    /// <summary>
    /// Annual interest rate or APR associated with the account.
    /// </summary>
    public decimal? InterestRate { get; set; }
}
