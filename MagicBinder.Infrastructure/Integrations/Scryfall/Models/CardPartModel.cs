namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record CardPartModel
{
    public Guid CardId { get; init; }
    public string Name { get; init; }
    public string Component { get; init; }
    public string TypeLine { get; init; }
}