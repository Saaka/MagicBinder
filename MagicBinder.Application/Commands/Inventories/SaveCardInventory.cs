﻿using MagicBinder.Application.Exceptions;
using MagicBinder.Application.Mappers;
using MagicBinder.Core.Requests;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Commands.Inventories;

public record SaveCardInventory(Guid OracleId, ICollection<SaveCardInventory.SavePrintingInfo> Printings) : Request
{
    public record SavePrintingInfo(Guid CardId, int Count, bool IsFoil);
}

public class SaveCardInventoryHandler : RequestHandler<SaveCardInventory, Guid>
{
    private readonly InventoriesRepository _inventoriesRepository;
    private readonly CardsRepository _cardsRepository;
    private readonly IRequestContextService _requestContextService;

    public SaveCardInventoryHandler(InventoriesRepository inventoriesRepository, CardsRepository cardsRepository, IRequestContextService requestContextService)
    {
        _inventoriesRepository = inventoriesRepository;
        _cardsRepository = cardsRepository;
        _requestContextService = requestContextService;
    }

    public override async Task<RequestResult<Guid>> Handle(SaveCardInventory request, CancellationToken cancellationToken)
    {
        if (!_requestContextService.CurrentContext.IsAuthorized) throw new UnauthorizedAccessException();
        if (!request.Printings.Any()) return request.Success();

        var card = await _cardsRepository.GetAsync(request.OracleId, cancellationToken);
        if (card == null) throw new ArgumentException(nameof(request.OracleId));

        var inventoryPrintings = new List<InventoryPrinting>();
        foreach (var toAdd in request.Printings.GroupBy(x => new { x.CardId, x.IsFoil }))
        {
            var printing = card.CardPrintings.FirstOrDefault(x => x.CardId == toAdd.Key.CardId);
            if (printing == null)
                throw new ArgumentException(nameof(SaveCardInventory.SavePrintingInfo.CardId));

            var inventoryPrinting = printing.MapToInventoryPrinting(toAdd.Sum(x => x.Count), toAdd.Key.IsFoil);
            inventoryPrintings.Add(inventoryPrinting);
        }

        var inventory = new Inventory(card.OracleId, _requestContextService.CurrentContext.User.Id, card.Name, inventoryPrintings);
        await _inventoriesRepository.UpsertAsync(inventory, cancellationToken);

        return request.Success();
    }
}