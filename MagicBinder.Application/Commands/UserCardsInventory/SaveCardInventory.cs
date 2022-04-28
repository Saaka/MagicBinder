using MagicBinder.Application.Exceptions;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Commands.UserCardsInventory;

public record SaveCardInventory(Guid OracleId, ICollection<SaveCardInventory.SavePrintingInfo> Printings) : Request
{
    public record SavePrintingInfo(Guid CardId, int Count, bool IsFoil);
}

public class SaveCardInventoryHandler : RequestHandler<SaveCardInventory, Guid>
{
    private readonly InventoryRepository _inventoryRepository;
    private readonly CardsRepository _cardsRepository;
    private readonly IRequestContextService _requestContextService;

    public SaveCardInventoryHandler(InventoryRepository inventoryRepository, CardsRepository cardsRepository, IRequestContextService requestContextService)
    {
        _inventoryRepository = inventoryRepository;
        _cardsRepository = cardsRepository;
        _requestContextService = requestContextService;
    }

    public override async Task<RequestResult<Guid>> Handle(SaveCardInventory request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.User.IsAuthorized)
            throw new UnauthorizedAccessException();

        var card = await _cardsRepository.GetAsync(request.OracleId);
        if (card == null)
            throw new CardNotFoundException(request.OracleId);

        foreach (var printingToAdd in request.Printings)
        {
            var printing = card.CardPrintings.FirstOrDefault(x => x.CardId == printingToAdd.CardId);
            if (printing == null)
                throw new CardPrintingNotFoundException(printingToAdd.CardId);
            
            
        }

        return request.Success();
    }
}