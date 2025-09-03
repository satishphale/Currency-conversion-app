public interface IExchangeRateProvider
{
    Task<LatestRatesResponse> GetLatestRatesAsync(string baseCurrency);
    Task<ConvertResponse> ConvertAsync(string from, string to, decimal amount);
    Task<IEnumerable<DailyRatesDto>> GetHistoricalRatesAsync(string baseCurrency, DateTime from, DateTime to);
}