using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("health-check")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("OK");
    }
}