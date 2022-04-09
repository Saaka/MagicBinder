using MagicBinder.Application.Commands.Auth;
using MagicBinder.Application.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("google")]
    public async Task<ActionResult<AuthorizationModel>> AuthorizeWithGoogle(AuthorizeWithGoogle command) => await _mediator.Send(command);
}