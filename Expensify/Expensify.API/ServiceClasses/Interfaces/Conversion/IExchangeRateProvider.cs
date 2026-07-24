namespace Expensify.API.ServiceClasses.Interfaces.Conversion;

public interface IExchangeRateProvider
{
    Task<decimal> GetExchangeRateAsync(
        string fromCurrencyCode,
        string toCurrencyCode,
        CancellationToken cancellationToken
    );
}
