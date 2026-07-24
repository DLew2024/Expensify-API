using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.ReferenceDataDTOs;

public class CategoryDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public CategoryType Type { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsSystemDefault { get; set; }

    public bool IsActive { get; set; }

    public static readonly Expression<Func<Category, CategoryDTO>> Projection =
        category => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Description = category.Description ?? string.Empty,
            IsSystemDefault = category.IsSystemDefault,
            IsActive = category.IsActive,
        };
}
