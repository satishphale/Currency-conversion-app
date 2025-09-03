[ApiController]
[Route("api/v1/rates")]
public class RatesController : ControllerBase
{
    private readonly IExchangeService _service;

    public RatesController(IExchangeService service)
    {
        _service = service;
    }

    [HttpGet("latest")]
    [Authorize(Roles = "reader")]
    public async Task<IActionResult> GetLatest([FromQuery] string baseCurrency = "EUR")
    {
        var result = await _service.GetLatestRatesAsync(baseCurrency);
        return Ok(result);
    }

    [HttpGet("history")]
    [Authorize(Roles = "reader")]
    public async Task<IActionResult> GetHistory([FromQuery] string baseCurrency, [FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetHistoricalRatesAsync(baseCurrency, from, to, page, pageSize);
        return Ok(result);
    }
}