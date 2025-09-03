public class ExchangeService : IExchangeService
{
    private static readonly HashSet<string> _excluded = ["TRY", "PLN", "THB", "MXN"];
    private readonly IExchangeRateProvider _provider;

    public ExchangeService(IExchangeRateProvider provider)
    {
        _provider = provider;
    }

    public async Task<LatestRatesResponse> GetLatestRatesAsync(string baseCurrency)
        => await _provider.GetLatestRatesAsync(baseCurrency);

    public async Task<ConvertResponse> ConvertAsync(ConvertRequest request)
    {
        if (_excluded.Contains(request.From) || _excluded.Contains(request.To))
            throw new ArgumentException("Currency not supported");

        return await _provider.ConvertAsync(request.From, request.To, request.Amount);
    }

    public async Task<PagedResponse<DailyRatesDto>> GetHistoricalRatesAsync(string baseCurrency, DateTime from, DateTime to, int page, int pageSize)
    {
        var allRates = await _provider.GetHistoricalRatesAsync(baseCurrency, from, to);
        var total = allRates.Count();
        var items = allRates.Skip((page - 1) * pageSize).Take(pageSize);

        return new PagedResponse<DailyRatesDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = total,
            Items = items
        };
    }
}