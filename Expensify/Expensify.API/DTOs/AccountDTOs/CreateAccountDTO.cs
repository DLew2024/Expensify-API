using System.ComponentModel.DataAnnotations;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
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
    public Guid? AccountTypeId { get; set; }

    /// <summary>
    /// The unique identifier of the currency used by the account.
    /// </summary>
    [Required]
    public Guid? CurrencyCodeId { get; set; }

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
    /// The initial balance of the account.
    /// </summary>
    [Required]
    public decimal InitialBalance { get; set; }

    /// <summary>
    /// Indicates whether the account should be included when calculating net worth.
    /// </summary>
    public bool IncludeInNetWorth { get; set; } = true;

    /// <summary>
    /// Optional notes associated with the account.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Optional Icon to add with the account.
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    public Account ToEntity(Guid userId)
    {
        if (AccountTypeId is null)
        {
            throw new ValidationException("An account type is required.");
        }

        if (CurrencyCodeId is null)
        {
            throw new ValidationException("A currency is required.");
        }

        return new Account
        {
            UserId = userId,
            Name = Name.Trim(),
            AccountTypeId = AccountTypeId.Value,
            CurrencyCodeId = CurrencyCodeId.Value,
            InstitutionName = InstitutionName.Trim(),
            LastFourDigits = LastFourDigits.Trim(),
            CurrentBalance = InitialBalance,
            AvailableBalance = InitialBalance,
            IncludeInNetWorth = IncludeInNetWorth,
            Notes = Notes.Trim(),
            Icon = Icon.Trim(),
            IsActive = true,
            IsHidden = false,
            IsDefault = false,
            CreatedBy = userId,
            LastUpdatedBy = userId,
        };
    }

    public static CreateAccountDTO FromEntity(Account account)
    {
        return new CreateAccountDTO
        {
            Name = account.Name,
            AccountTypeId = account.AccountTypeId,
            InstitutionName = account.InstitutionName,
            LastFourDigits = account.LastFourDigits ?? string.Empty,
            InitialBalance = account.CurrentBalance,
            IncludeInNetWorth = account.IncludeInNetWorth,
            Notes = account.Notes ?? string.Empty,
            Icon = account.Icon ?? string.Empty,
        };
    }
}
