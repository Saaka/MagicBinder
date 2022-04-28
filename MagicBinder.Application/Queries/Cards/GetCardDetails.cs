using MagicBinder.Application.Exceptions;
using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Cards;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Queries.Cards;

public record GetCardDetails(Guid OracleId) : Request<CardDetailsModel>;

public class GetCardDetailsQueryHandler : RequestHandler<GetCardDetails, CardDetailsModel>
{
    private readonly CardsRepository _cardsRepository;

    public GetCardDetailsQueryHandler(CardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public override async Task<RequestResult<CardDetailsModel>> Handle(GetCardDetails request, CancellationToken cancellationToken)
    {
        var card = await _cardsRepository.GetAsync(request.OracleId, cancellationToken);
        if (card == null) throw new CardNotFoundException(request.OracleId);

        return request.Success(card.MapToCardDetails());
    }
}