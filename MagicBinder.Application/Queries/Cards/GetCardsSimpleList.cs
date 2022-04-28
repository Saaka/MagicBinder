using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Cards;
using MagicBinder.Core.Models;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Cards;

public record GetCardsSimpleList : Request<PagedList<CardInfoModel>>, IPageableRequest
{
    public string? Name { get; init; }
    public string? TypeLine { get; init; }
    public string? OracleText { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
}

public class GetCardsInfoQueryHandler : RequestHandler<GetCardsSimpleList, PagedList<CardInfoModel>>
{
    private readonly CardsRepository _cardsRepository;

    public GetCardsInfoQueryHandler(CardsRepository cardsRepository)

    {
        _cardsRepository = cardsRepository;
    }

    public override async Task<RequestResult<PagedList<CardInfoModel>>> Handle(GetCardsSimpleList request, CancellationToken cancellationToken)
    {
        var cardsList = await _cardsRepository.GetCardsListAsync(request.MapToQueryParams());

        return request.Success(cardsList.MapToResponse(CardMapper.MapToCardInfo));
    }
}