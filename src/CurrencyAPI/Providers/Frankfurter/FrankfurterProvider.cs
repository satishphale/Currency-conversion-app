public class FrankfurterProvider : IExchangeRateProvider
{
    private readonly HttpClient _httpClient;

    public FrankfurterProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LatestRatesResponse> GetLatestRatesAsync(string baseCurrency)
    {
        var response = await _httpClient.GetFromJsonAsync<LatestRatesResponse>($"latest?base={baseCurrency}");
        return response!;
    }

    public async Task<ConvertResponse> ConvertAsync(string from, string to, decimal amount)
    {
        var url = $"latest?amount={amount}&from={from}&to={to}";
        var data = await _httpClient.GetFromJsonAsync<JsonDocument>(url);

        var rate = data.RootElement.GetProperty("rates").GetProperty(to).GetDecimal();
        var converted = rate;

        return new ConvertResponse
        {
            From = from,
            To = to,
            OriginalAmount = amount,
            ConvertedAmount = converted,
            Rate = rate / amount
        };
    }

    public async Task<IEnumerable<DailyRatesDto>> GetHistoricalRatesAsync(string baseCurrency, DateTime from, DateTime to)
    {
        var url = $"{from:yyyy-MM-dd}..{to:yyyy-MM-dd}?base={baseCurrency}";
        var data = await _httpClient.GetFromJsonAsync<JsonDocument>(url);

        var results = new List<DailyRatesDto>();
        foreach (var property in data.RootElement.GetProperty("rates").EnumerateObject())
        {
            results.Add(new DailyRatesDto
            {
                Date = DateTime.Parse(property.Name),
                Rates = property.Value.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.GetDecimal())
            });
        }
        return results;
    }
}