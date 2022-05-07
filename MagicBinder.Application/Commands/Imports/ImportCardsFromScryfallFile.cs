using MagicBinder.Application.Mappers.Importers;
using MagicBinder.Core.Requests;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using MagicBinder.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace MagicBinder.Application.Commands.Imports;

public record ImportCardsFromScryfallFile(string JsonFileContent) : Request;

public class ImportCardsFromScryfallFileHandler : RequestHandler<ImportCardsFromScryfallFile, Guid>
{
    private readonly JsonCardsParser _jsonCardsParser;
    private readonly CardsRepository _cardsRepository;
    private readonly ILogger _logger;
    private const int MaxBatchSize = 1000;

    public ImportCardsFromScryfallFileHandler(JsonCardsParser jsonCardsParser, CardsRepository cardsRepository, ILogger<ImportCardsFromScryfallFileHandler> logger)
    {
        _jsonCardsParser = jsonCardsParser;
        _cardsRepository = cardsRepository;
        _logger = logger;
    }

    public override async Task<RequestResult<Guid>> Handle(ImportCardsFromScryfallFile request, CancellationToken cancellationToken)
    {
        var cards = _jsonCardsParser.ParseCards(request.JsonFileContent);
        var groupedCards = ReturnedFilteredGroups(cards);
        var cardsToSave = new List<Card>();
        var allSavedCards = new List<Card>();
        var batchNumber = 0;
        while (true)
        {
            var cardBatch = groupedCards.Skip(batchNumber * MaxBatchSize).Take(MaxBatchSize).ToList();
            foreach (var cardGroup in cardBatch)
            {
                var printingModels = cardGroup.ToList();
                printingModels = printingModels.OrderByDescending(x => x.ReleasedAt).ToList();
                var latestPrintingModel = printingModels.First();

                var card = latestPrintingModel.MapToCard();
                var printings = printingModels.Select(x => x.MapToCardPrinting()).ToList();
                card.LatestPrinting = printings.First(x => x.Games.Contains(GameType.Paper));
                card.CardPrintings = printings;
                _ = card.MapMissingFields();

                cardsToSave.Add(card);
            }

            if (!cardsToSave.Any()) break;

            batchNumber++;

            _logger.LogInformation("Saving batch number {BatchNumber} with {CardsCount} cards", batchNumber, cardsToSave.Count);
            await _cardsRepository.UpsertManyAsync(cardsToSave, cancellationToken);
            allSavedCards.AddRange(cardsToSave);
            cardsToSave.Clear();
        }

        _logger.LogInformation("Saved {CardsCount} cards", allSavedCards.Count);
        await UpdateRelatedParts(allSavedCards, cancellationToken);

        _logger.LogInformation("Import finished");
        return request.Success();
    }

    private async Task UpdateRelatedParts(List<Card> allSavedCards, CancellationToken cancellationToken)
    {
        var cardsToUpdateParts = allSavedCards.Where(sc => sc.CardPrintings.Any(cp => cp.AllParts.Any(p => p.OracleId == p.CardId))).ToList();
        var batchNumber = 0;
        var cardsToSave = new List<Card>();
        var cardDictionary = new Dictionary<Guid, CardPrinting>();

        _logger.LogInformation("Updating {CardsCount} cards with missing parts data", cardsToUpdateParts.Count);
        while (true)
        {
            var cardBatch = cardsToUpdateParts.Skip(batchNumber * MaxBatchSize).Take(MaxBatchSize).ToList();
            foreach (var cardToUpdate in cardBatch)
            {
                var shouldSave = false;
                foreach (var printing in cardToUpdate.CardPrintings)
                {
                    foreach (var part in printing.AllParts)
                    {
                        var partPrinting = GetPartPrinting(allSavedCards, part, cardDictionary);
                        if (partPrinting == null) continue;

                        part.OracleId = partPrinting.OracleId;
                        part.Rarity = partPrinting.Rarity;

                        shouldSave = true;
                    }
                }

                if (shouldSave)
                    cardsToSave.Add(cardToUpdate);
            }

            if (!cardBatch.Any()) break;

            batchNumber++;
            if (!cardsToSave.Any())
            {
                _logger.LogInformation("Batch number {BatchNumber} has no cards to save.", batchNumber);
                continue;
            }

            _logger.LogInformation("Saving batch number {BatchNumber} with {CardsCount} cards to update", batchNumber, cardsToSave.Count);
            await _cardsRepository.UpsertManyAsync(cardsToSave, cancellationToken);
            cardsToSave.Clear();
        }
    }

    private static CardPrinting? GetPartPrinting(List<Card> allSavedCards, CardPart part, Dictionary<Guid, CardPrinting> dictionary)
    {
        if (dictionary.TryGetValue(part.CardId, out var partPrinting)) return partPrinting;
        
        var card = allSavedCards.FirstOrDefault(x => x.CardPrintings.Any(p => p.CardId == part.CardId));
        partPrinting = card?.CardPrintings.FirstOrDefault(x => x.CardId == part.CardId);
        if (partPrinting == null) return null;
            
        dictionary.Add(part.CardId, partPrinting);
        return partPrinting;
    }

    private static List<IGrouping<Guid, CardModel>> ReturnedFilteredGroups(List<CardModel> cards) =>
        cards.Where(x =>
                !x.SetName.ToLower().Contains("minigames") &&
                !x.Oversized &&
                x.SetType != "memorabilia" &&
                Layouts.Contains(x.Layout) &&
                x.Games.Contains(ScryfallCardsConstants.Games.Paper))
            .GroupBy(x => x.OracleId).ToList();

    private static string[] Layouts => typeof(ScryfallCardsConstants.Layouts).GetFields().Select(x => x.GetValue(x).ToString()).ToArray();
}