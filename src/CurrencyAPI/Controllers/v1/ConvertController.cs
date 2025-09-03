[ApiController]
[Route("api/v1/convert")]
public class ConvertController : ControllerBase
{
    private readonly IExchangeService _service;

    public ConvertController(IExchangeService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "converter")]
    public async Task<IActionResult> Convert([FromBody] ConvertRequest request)
    {
        try
        {
            var result = await _service.ConvertAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}