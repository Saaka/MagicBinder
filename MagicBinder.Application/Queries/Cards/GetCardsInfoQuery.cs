using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Cards;
using MagicBinder.Core.Models;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Cards;

public class GetCardsInfoQuery : Request<PagedList<CardInfoModel>>, IPageableRequest
{
    public string? Name { get; set; }
    public string? TypeLine { get; set; }
    public string? OracleText { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class GetCardsInfoQueryHandler : RequestHandler<GetCardsInfoQuery, PagedList<CardInfoModel>>
{
    private readonly CardsRepository _cardsRepository;

    public GetCardsInfoQueryHandler(CardsRepository cardsRepository)

    {
        _cardsRepository = cardsRepository;
    }

    public override async Task<RequestResult<PagedList<CardInfoModel>>> Handle(GetCardsInfoQuery request, CancellationToken cancellationToken)
    {
        var cardsList = await _cardsRepository.GetCardsListAsync(request.MapToQueryParams());

        return request.Success(cardsList.MapToResponse(CardMapper.MapToCardInfo));
    }
}