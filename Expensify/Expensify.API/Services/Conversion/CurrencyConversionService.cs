using Expensify.API.Services.Interfaces.Conversion;

namespace Expensify.API.Services.Conversion;

public class CurrencyConversionService(IExchangeRateProvider exchangeRateProvider)
    : ICurrencyConversionService
{
    private readonly IExchangeRateProvider _exchangeRateProvider = exchangeRateProvider;

    public async Task<decimal> ConvertAsync(
        decimal amount,
        string fromCurrencyCode,
        string toCurrencyCode,
        CancellationToken cancellationToken
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fromCurrencyCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(toCurrencyCode);

        var normalizedFromCurrencyCode = fromCurrencyCode.Trim().ToUpperInvariant();

        var normalizedToCurrencyCode = toCurrencyCode.Trim().ToUpperInvariant();

        if (normalizedFromCurrencyCode == normalizedToCurrencyCode)
        {
            return amount;
        }

        var exchangeRate = await _exchangeRateProvider.GetExchangeRateAsync(
            normalizedFromCurrencyCode,
            normalizedToCurrencyCode,
            cancellationToken
        );

        return decimal.Round(amount * exchangeRate, 2, MidpointRounding.AwayFromZero);
    }
}
