using System.Linq.Expressions;
using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;

namespace Expensify.API.DTOs.ReferenceDataDTOs
{
    public class CurrencyCodeDTO
    {
        public required Guid Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public required string Symbol { get; set; }
        public int DecimalPlaces { get; set; }

        public static readonly Expression<Func<CurrencyCode, CurrencyCodeDTO>> Projection =
            currency => new CurrencyCodeDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.Name,
                Symbol = currency.Symbol,
                DecimalPlaces = currency.DecimalPlaces,
            };
    }
}
