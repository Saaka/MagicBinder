using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Cards;
using MagicBinder.Core.Models;
using MagicBinder.Infrastructure.Repositories;
using MediatR;

namespace MagicBinder.Application.Queries.Cards;

public record GetCardsInfoQuery : IRequest<PagedList<CardInfoModel>>, IPageableRequest
{
    public string Filter { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class GetCardsInfoQueryHandler : IRequestHandler<GetCardsInfoQuery, PagedList<CardInfoModel>>
{
    private readonly CardsRepository _cardsRepository;

    public GetCardsInfoQueryHandler(CardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<PagedList<CardInfoModel>> Handle(GetCardsInfoQuery request, CancellationToken cancellationToken)
    {
        var cardsList = await _cardsRepository.GetCardsListAsync(request.Filter, request);

        return cardsList.MapToResponse(CardMapper.MapToCardInfo);
    }
}