using MagicBinder.Application.Mappers;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicBinder.Application.Commands.Cards;

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
        var cardsToSave = new List<Card>();
        var batchNumber = 0;
        while (true)
        {
            var cardBatch = cards.Skip(batchNumber * MaxBatchSize).Take(MaxBatchSize).ToList();
            foreach (var cardModel in cardBatch)
            {
                var card = await _cardsRepository.GetByCardIdAsync(cardModel.CardId);
                card = cardModel.MapToCard(card);
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
}