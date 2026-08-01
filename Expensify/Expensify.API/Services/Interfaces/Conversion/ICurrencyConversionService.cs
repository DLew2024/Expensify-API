namespace Expensify.API.Services.Interfaces.Conversion;

public interface ICurrencyConversionService
{
    Task<decimal> ConvertAsync(
        decimal amount,
        string fromCurrency,
        string toCurrency,
        CancellationToken cancellationToken
    );
}
