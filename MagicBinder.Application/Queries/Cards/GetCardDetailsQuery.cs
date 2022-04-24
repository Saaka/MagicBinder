using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Cards;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Cards;

public class GetCardDetailsQuery : Request<CardDetailsModel>
{
    public Guid OracleId { get; init; }
}

public class GetCardDetailsQueryHandler : RequestHandler<GetCardDetailsQuery, CardDetailsModel>
{
    private readonly CardsRepository _cardsRepository;

    public GetCardDetailsQueryHandler(CardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public override async Task<RequestResult<CardDetailsModel>> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
    {
        var card = await _cardsRepository.GetAsync(request.OracleId);

        return request.Success(card.MapToCardDetails());
    }
}