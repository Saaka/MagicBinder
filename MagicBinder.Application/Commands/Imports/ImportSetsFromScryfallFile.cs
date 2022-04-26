using MagicBinder.Core.Requests;

namespace MagicBinder.Application.Commands.Imports;

public record ImportSetsFromScryfallFile(string JsonFileContent) : Request;

public class ImportSetsFromScryfallFileHandler : RequestHandler<ImportSetsFromScryfallFile, Guid>
{
    public ImportSetsFromScryfallFileHandler()
    {
    }

    public override async Task<RequestResult<Guid>> Handle(ImportSetsFromScryfallFile request, CancellationToken cancellationToken)
    {
        return request.Success();
    }
}