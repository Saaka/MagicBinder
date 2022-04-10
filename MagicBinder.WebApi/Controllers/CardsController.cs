using MagicBinder.Application.Models.Cards;
using MagicBinder.Application.Queries.Cards;
using MagicBinder.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("list/simple")]
    public async Task<ActionResult<PagedList<CardInfoModel>>> GetCards(GetCardsInfoQuery query) => await _mediator.Send(query);
}