using MagicBinder.Application.Commands.Decks;
using MagicBinder.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DecksController
{
    private readonly MediatorCommandSender _mediator;

    public DecksController(MediatorCommandSender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> CreateDeck(CreateDeck command) => await _mediator.SendCommand(command);
}