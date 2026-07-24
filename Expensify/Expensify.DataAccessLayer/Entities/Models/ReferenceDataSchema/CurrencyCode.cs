using Expensify.DataAccessLayer.Entities.AbstractClasses;
using Expensify.DataAccessLayer.Entities.Models.FinanceSchema;

namespace Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;

public class CurrencyCode : Auditable
{
    public CurrencyCode() { }

    public CurrencyCode(string name, string code, string symbol)
        : base(name)
    {
        Code = code;
        Symbol = symbol;
    }

    public string Code { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public int DecimalPlaces { get; set; } = 2;

    public bool IsActive { get; set; } = true;
}
