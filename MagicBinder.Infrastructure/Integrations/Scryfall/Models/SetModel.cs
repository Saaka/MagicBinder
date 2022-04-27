namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record SetModel
{
    public Guid SetId { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public string ScryfallUri { get; init; }
    public string RealeasedAt { get; init; }
    public string SetType { get; init; }
    public int CardCount { get; init; }
    public string Icon { get; init; }
}

public class SetsImportModel
{
    public List<SetModel> Sets { get; init; } = new();
}