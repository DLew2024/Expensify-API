using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;

namespace Expensify.API.DTOs.ReferenceDataDTOs;

public class PaymentMethodDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsSystemDefault { get; set; }

    public static readonly Expression<Func<PaymentMethod, PaymentMethodDTO>> Projection =
        paymentMethod => new PaymentMethodDTO
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name,
            Description = paymentMethod.Description ?? string.Empty,
            IsSystemDefault = paymentMethod.IsSystemDefault,
        };
}
