using System.Net;
using MagicBinder.Core.Requests;
using MagicBinder.Core.Requests.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("health-check")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;
    private readonly IRequestContextService _requestContextService;

    public HealthCheckController(ILogger<HealthCheckController> logger, IRequestContextService requestContextService)
    {
        _logger = logger;
        _requestContextService = requestContextService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = "OK";
        _logger.LogInformation("Returning \"{result}\" result.", result);

        return Ok(result);
    }

    [HttpGet]
    [Route("user")]
    [ProducesResponseType(typeof(AuthContextModel), (int) HttpStatusCode.OK)]
    public IActionResult GetUser() => Ok(_requestContextService.User);
}