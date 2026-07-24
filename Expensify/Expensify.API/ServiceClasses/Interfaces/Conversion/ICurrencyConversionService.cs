namespace Expensify.API.ServiceClasses.Interfaces.Conversion
{
    public interface ICurrencyConversionService
    {
        Task<decimal> ConvertAsync(
            decimal amount,
            string fromCurrency,
            string toCurrency,
            CancellationToken cancellationToken
        );
    }
}
