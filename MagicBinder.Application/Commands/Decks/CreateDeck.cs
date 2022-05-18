using MagicBinder.Application.Exceptions;
using MagicBinder.Application.Mappers;
using MagicBinder.Application.Services;
using MagicBinder.Core.Requests;
using MagicBinder.Core.Services;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Commands.Decks;

public record CreateDeck(string Name, GameType GameType, FormatType Format) : Request<Guid>;

public class CreateDeckHandler : RequestHandler<CreateDeck, Guid>
{
    private readonly DecksRepository _decksRepository;
    private readonly GuidService _guidService;
    private readonly DefaultDeckCardCategoriesService _deckCardCategoriesService;
    private readonly IRequestContextService _requestContextService;

    public CreateDeckHandler(DecksRepository decksRepository, GuidService guidService, DefaultDeckCardCategoriesService deckCardCategoriesService, IRequestContextService requestContextService)
    {
        _decksRepository = decksRepository;
        _guidService = guidService;
        _deckCardCategoriesService = deckCardCategoriesService;
        _requestContextService = requestContextService;
    }

    public override async Task<RequestResult<Guid>> Handle(CreateDeck request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.CurrentContext.IsAuthorized)
            throw new UnauthorizedAccessException();
        if (await _decksRepository.IsNameInUse(request.Name, _requestContextService.CurrentContext.User.Id, cancellationToken))
            throw new DuplicatedDeckNameException(request.Name);

        var deck = request.MapToAggregate(_guidService.GetGuid(),
            _requestContextService.CurrentContext.User.Id,
            _deckCardCategoriesService.GetDefaultCardCategories(request.Format));

        await _decksRepository.InsertAsync(deck, cancellationToken);

        return request.Success(deck.DeckId);
    }
}