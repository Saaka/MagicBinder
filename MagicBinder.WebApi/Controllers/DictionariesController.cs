using MagicBinder.Application.Models.Dictionaries;
using MagicBinder.Application.Queries.Dictionaries;
using MagicBinder.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DictionariesController: ControllerBase
{
    private readonly MediatorCommandSender _mediator;

    public DictionariesController(MediatorCommandSender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("sets")]
    public async Task<ActionResult<ICollection<SetModel>>> GetCardDetails() => await _mediator.SendQuery(new GetSets());
}