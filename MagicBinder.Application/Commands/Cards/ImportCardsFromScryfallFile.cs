using MagicBinder.Infrastructure.Integrations.Scryfall;
using MediatR;

namespace MagicBinder.Application.Commands.Cards;

public record ImportCardsFromScryfallFile(string JsonFileContent) : IRequest;

public class ImportCardsFromScryfallFileHandler : IRequestHandler<ImportCardsFromScryfallFile>
{
    private readonly JsonCardsParser _jsonCardsParser;

    public ImportCardsFromScryfallFileHandler(JsonCardsParser jsonCardsParser)
    {
        _jsonCardsParser = jsonCardsParser;
    }
    
    public async Task<Unit> Handle(ImportCardsFromScryfallFile request, CancellationToken cancellationToken)
    {
        var cards = _jsonCardsParser.ParseCards(request.JsonFileContent);

        
        return Unit.Value;
    }
}