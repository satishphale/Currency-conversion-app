public interface IExchangeService
{
    Task<LatestRatesResponse> GetLatestRatesAsync(string baseCurrency);
    Task<ConvertResponse> ConvertAsync(ConvertRequest request);
    Task<PagedResponse<DailyRatesDto>> GetHistoricalRatesAsync(string baseCurrency, DateTime from, DateTime to, int page, int pageSize);
}