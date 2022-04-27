using MagicBinder.Application.Mappers;
using MagicBinder.Core.Requests;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using MagicBinder.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace MagicBinder.Application.Commands.Imports;

public record ImportSetsFromScryfallFile(string JsonFileContent) : Request;

public class ImportSetsFromScryfallFileHandler : RequestHandler<ImportSetsFromScryfallFile, Guid>
{
    private readonly SetsRepository _setsRepository;
    private readonly JsonSetsParser _setsParser;
    private readonly ILogger<ImportSetsFromScryfallFileHandler> _logger;
    private const int MaxBatchSize = 100;

    public ImportSetsFromScryfallFileHandler(SetsRepository setsRepository, JsonSetsParser setsParser, ILogger<ImportSetsFromScryfallFileHandler> logger)
    {
        _setsRepository = setsRepository;
        _setsParser = setsParser;
        _logger = logger;
    }

    public override async Task<RequestResult<Guid>> Handle(ImportSetsFromScryfallFile request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Parsing sets file...");
        var setModels = _setsParser.ParseSets(request.JsonFileContent);
        var toImport = GetFilteredSets(setModels);
        _logger.LogInformation("Importing {totalCount} sets...", toImport.Count);

        var batchToSave = new List<Set>();
        var batchNumber = 0;
        while (true)
        {
            var setBatch = toImport.Skip(batchNumber * MaxBatchSize).Take(MaxBatchSize).ToList();
            batchToSave.AddRange(setBatch.Select(setModel => setModel.MapToAggregate()));
            
            if (!batchToSave.Any()) break;

            batchNumber++;
            _logger.LogInformation("Saving batch number {0} with {1} sets", batchNumber, batchToSave.Count);
            await _setsRepository.UpsertManyAsync(batchToSave);
            batchToSave.Clear();
        }

        _logger.LogInformation("Import finished");
        return request.Success();
    }

    private List<SetModel> GetFilteredSets(List<SetModel> setModels) =>
        setModels
            .Where(x => SetTypes.Contains(x.SetType))
            .OrderByDescending(x => x.RealeasedAt)
            .ToList();

    private static string[] SetTypes => typeof(ScryfallSetsConstants.SetTypes).GetFields().Select(x => x.GetValue(x).ToString()).ToArray();
}