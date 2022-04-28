using System.Net;
using MagicBinder.Core.Requests;
using MagicBinder.Core.Requests.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("test")]
public class TestController: ControllerBase
{
    private readonly IRequestContextService _requestContextService;

    public TestController(IRequestContextService requestContextService)
    {
        _requestContextService = requestContextService;
    }
    
    [HttpGet]
    [Route("user")]
    [ProducesResponseType(typeof(AuthContextModel), (int) HttpStatusCode.OK)]
    public IActionResult GetUser() => Ok(_requestContextService.CurrentContext);
    
    [HttpGet]
    [Authorize]
    [Route("user-auth")]
    [ProducesResponseType(typeof(AuthContextModel), (int) HttpStatusCode.OK)]
    public IActionResult GetUserWithAuth() => Ok(_requestContextService.CurrentContext);
}