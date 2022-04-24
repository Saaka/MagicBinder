using MagicBinder.Application.Mappers;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Enums;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using MagicBinder.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicBinder.Application.Commands.Imports;

public record ImportCardsFromScryfallFile(string JsonFileContent) : IRequest;

public class ImportCardsFromScryfallFileHandler : IRequestHandler<ImportCardsFromScryfallFile>
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

    public async Task<Unit> Handle(ImportCardsFromScryfallFile request, CancellationToken cancellationToken)
    {
        var cards = _jsonCardsParser.ParseCards(request.JsonFileContent);
        var groupedCards = ReturnedFilteredGroups(cards);
        var cardsToSave = new List<Card>();
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

            _logger.LogInformation("Saving batch number {0} with {1} cards", batchNumber, cardsToSave.Count);
            await _cardsRepository.UpsertManyAsync(cardsToSave);
            cardsToSave.Clear();
        }

        _logger.LogInformation("Import finished");
        return Unit.Value;
    }

    private static List<IGrouping<Guid, CardModel>> ReturnedFilteredGroups(List<CardModel> cards) =>
        cards.Where(x => x.SetName != "token"
                         && !x.Oversized &&
                         x.SetType != "memorabilia"
                         && Layouts.Contains(x.Layout)
                         && x.Games.Contains(ScryfallConstants.Games.Paper))
            .GroupBy(x => x.OracleId).ToList();

    private static string[] Layouts => typeof(ScryfallConstants.Layouts).GetFields().Select(x => x.GetValue(x).ToString()).ToArray();
}