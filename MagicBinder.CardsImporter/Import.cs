using MagicBinder.Application.Commands.Imports;
using MagicBinder.Core.Requests;

namespace MagicBinder.CardsImporter;

public record Import
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string DefaultFileName { get; init; } = string.Empty;
    public Func<string, Request> RequestFactory { get; init; }

    public static List<Import> GetImports() => new()
    {
        new Import
        {
            Id = "card",
            Name = "Cards Import",
            DefaultFileName = "Import.json",
            RequestFactory = content => new ImportCardsFromScryfallFile(content)
        },
        new Import
        {
            Id = "set",
            Name = "Sets Import",
            DefaultFileName = "Sets.json",
            RequestFactory = content => new ImportSetsFromScryfallFile(content)
        }
    };
}