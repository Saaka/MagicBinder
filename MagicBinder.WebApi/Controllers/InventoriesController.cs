using MagicBinder.Application.Commands.Inventories;
using MagicBinder.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController
{
    private readonly MediatorCommandSender _mediator;

    public InventoriesController(MediatorCommandSender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> SaveInventory(SaveCardInventory command) => await _mediator.SendCommand(command);
}