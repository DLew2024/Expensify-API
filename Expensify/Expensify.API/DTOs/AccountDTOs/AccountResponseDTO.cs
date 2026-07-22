using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.AccountDTOs;

/// <summary>
/// Represents an account returned by the API.
/// </summary>
public class AccountResponseDTO
{
    /// <summary>
    /// The unique identifier of the account.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// The display name of the account.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The financial institution associated with the account.
    /// </summary>
    public string InstitutionName { get; set; } = string.Empty;

    /// <summary>
    /// The last four digits associated with the account.
    /// </summary>
    public string LastFourDigits { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the account type.
    /// </summary>
    public Guid AccountTypeId { get; set; }

    /// <summary>
    /// The name of the account type.
    /// </summary>
    public string AccountTypeName { get; set; } = string.Empty;

    /// <summary>
    /// The currency code used by the account.
    /// </summary>
    public CurrencyCode CurrencyCode { get; set; } = 0;

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// The currently available balance of the account.
    /// </summary>
    public decimal AvailableBalance { get; set; }

    /// <summary>
    /// Indicates whether the account is included in net worth calculations.
    /// </summary>
    public bool IncludeInNetWorth { get; set; }

    /// <summary>
    /// Indicates whether the account is currently active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Indicates whether the account is hidden from standard views.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// The optional credit limit associated with the account.
    /// </summary>
    public decimal? CreditLimit { get; set; }

    /// <summary>
    /// The optional interest rate associated with the account.
    /// </summary>
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Additional notes associated with the account.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Icon associated with the account.
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Projection used to convert an account entity into an account response DTO.
    /// </summary>
    public static readonly Expression<Func<Account, AccountResponseDTO>> Projection =
        account => new AccountResponseDTO
        {
            Id = account.Id,
            Name = account.Name,
            InstitutionName = account.InstitutionName ?? string.Empty,
            LastFourDigits = account.LastFourDigits ?? string.Empty,
            AccountTypeId = account.AccountTypeId,
            AccountTypeName = account.AccountType.Name,
            CurrencyCode = account.CurrencyCode,
            CurrentBalance = account.CurrentBalance,
            AvailableBalance = account.AvailableBalance,
            IncludeInNetWorth = account.IncludeInNetWorth,
            IsActive = account.IsActive,
            IsHidden = account.IsHidden,
            CreditLimit = account.CreditLimit,
            InterestRate = account.InterestRate,
            Notes = account.Notes ?? string.Empty,
            Icon = account.Icon ?? string.Empty
        };
}
