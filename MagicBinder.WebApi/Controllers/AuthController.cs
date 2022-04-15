using MagicBinder.Application.Commands.Auth;
using MagicBinder.Application.Models.Auth;
using MagicBinder.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly MediatorCommandSender _mediator;

    public AuthController(MediatorCommandSender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("google")]
    public async Task<ActionResult<AuthorizationModel>> AuthorizeWithGoogle(AuthorizeWithGoogle command) => await _mediator.SendCommand(command);
}