using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.ReferenceDataDTOs
{
    public class CurrencyCodeDTO
    {
        public required CurrencyCode Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public required string Symbol { get; set; }
    }
}
