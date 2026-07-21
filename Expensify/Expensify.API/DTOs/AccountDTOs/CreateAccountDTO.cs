using System.ComponentModel.DataAnnotations;
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
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the account type.
    /// </summary>
    [Required]
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// The name of the financial institution.
    /// Example: Chase, Fidelity, or Capital One.
    /// </summary>
    [Required]
    public string InstitutionName { get; set; } = string.Empty;

    /// <summary>
    /// The last four digits of the account number for display purposes.
    /// </summary>
    [Required]
    public string LastFourDigits { get; set; } = string.Empty;

    /// <summary>
    /// The currency used by the account.
    /// </summary>
    [Required]
    public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.USD;

    /// <summary>
    /// The initial balance of the account.
    /// </summary>
    [Required]
    public decimal InitialBalance { get; set; }

    /// <summary>
    /// Indicates whether the account should be included when calculating net worth.
    /// </summary>
    public bool IncludeInNetWorth { get; set; } = true;

    /// <summary>
    /// Maximum borrowable amount for credit-based accounts.
    /// </summary>
    public decimal CreditLimit { get; set; } = 0;

    /// <summary>
    /// Annual interest rate or APR associated with the account.
    /// </summary>
    public decimal InterestRate { get; set; } = 0;

    /// <summary>
    /// Optional notes associated with the account.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Optional Icon to add with the account.
    /// </summary>
    public string Icon { get; set; } = string.Empty;
}
