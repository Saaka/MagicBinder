using MagicBinder.Application.Commands.Inventories;
using MagicBinder.Application.Models.Inventories;
using MagicBinder.Application.Queries.Inventories;
using MagicBinder.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[Authorize]
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
    public async Task<ActionResult> SaveInventory(SaveCardInventory command) => await _mediator.SendCommand(command);

    [HttpGet("cards/{oracleId:guid}")]
    public async Task<ActionResult<CardInventoryModel>> GetCardInventory(Guid oracleId) => await _mediator.SendQuery(new GetCardInventory(oracleId));
}