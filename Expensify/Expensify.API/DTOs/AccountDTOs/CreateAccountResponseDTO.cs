using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.AccountDTOs;

/// <summary>
/// Represents a newly created financial account.
/// </summary>
public class CreateAccountResponseDTO
{
    /// <summary>
    /// The unique identifier of the account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// The user-defined name of the account.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the account type.
    /// </summary>
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// The display name of the account type.
    /// </summary>
    public string AccountTypeName { get; set; } = string.Empty;

    /// <summary>
    /// The name of the financial institution.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// The last four digits of the account number.
    /// </summary>
    public string? LastFourDigits { get; set; }

    /// <summary>
    /// The currency used by the account.
    /// </summary>
    public CurrencyCode CurrencyCode { get; set; }

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// The amount currently available to spend.
    /// </summary>
    public decimal AvailableBalance { get; set; }

    /// <summary>
    /// Indicates whether the account is included in net worth calculations.
    /// </summary>
    public bool IncludeInNetWorth { get; set; }

    /// <summary>
    /// Indicates whether the account is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Indicates whether the account is hidden.
    /// </summary>
    public bool IsHidden { get; set; }

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

    /// <summary>
    /// The date the account was created.
    /// </summary>
    public long CreateDate { get; set; }

    public static CreateAccountResponseDTO FromEntity(Account account, string accountTypeName)
    {
        return new CreateAccountResponseDTO
        {
            AccountId = account.Id,
            Name = account.Name,
            AccountTypeId = account.AccountTypeId,
            AccountTypeName = accountTypeName,
            InstitutionName = account.InstitutionName,
            LastFourDigits = account.LastFourDigits,
            CurrencyCode = account.CurrencyCode,
            CurrentBalance = account.CurrentBalance,
            AvailableBalance = account.AvailableBalance,
            IncludeInNetWorth = account.IncludeInNetWorth,
            IsActive = account.IsActive,
            IsHidden = account.IsHidden,
            Notes = account.Notes,
            CreditLimit = account.CreditLimit,
            InterestRate = account.InterestRate,
            CreateDate = account.CreateDate,
        };
    }
}
