using Expensify.API.ServiceClasses.Interfaces.Conversion;

namespace Expensify.API.ServiceClasses.Conversion;

public class ExchangeRateProvider : IExchangeRateProvider
{
    public Task<decimal> GetExchangeRateAsync(
        string fromCurrencyCode,
        string toCurrencyCode,
        CancellationToken cancellationToken
    )
    {
        var exchangeRate = (fromCurrencyCode, toCurrencyCode) switch
        {
            ("USD", "EUR") => 0.85m,
            ("EUR", "USD") => 1.18m,

            ("USD", "GBP") => 0.74m,
            ("GBP", "USD") => 1.35m,

            ("USD", "CAD") => 1.37m,
            ("CAD", "USD") => 0.73m,

            _ => throw new InvalidOperationException(
                $"Exchange rate from {fromCurrencyCode} to {toCurrencyCode} is unavailable."
            ),
        };

        return Task.FromResult(exchangeRate);
    }
}

// Later, replace this implementation with something like:
//public class ExchangeRateProvider : IExchangeRateProvider
//{
//    private readonly HttpClient _httpClient;

//    public ExchangeRateProvider(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//    }

//    public async Task<decimal> GetExchangeRateAsync(
//        string fromCurrencyCode,
//        string toCurrencyCode,
//        CancellationToken cancellationToken
//    )
//    {
//        // Call exchange-rate API
//        // Parse response
//        // Return current rate

//        throw new NotImplementedException();
//    }
//}
