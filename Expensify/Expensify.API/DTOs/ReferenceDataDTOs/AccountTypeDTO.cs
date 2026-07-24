using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.API.DTOs.ReferenceDataDTOs;

public class AccountTypeDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsSystemDefault { get; set; }

    public bool IsActive { get; set; }

    public static readonly Expression<Func<AccountType, AccountTypeDTO>> Projection =
        accountType => new AccountTypeDTO
        {
            Id = accountType.Id,
            Name = accountType.Name,
            Description = accountType.Description ?? string.Empty,
            IsSystemDefault = accountType.IsSystemDefault,
            IsActive = accountType.IsActive,
        };
}
