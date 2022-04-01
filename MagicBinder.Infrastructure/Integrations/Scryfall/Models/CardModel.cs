namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record CardModel
{
    public Guid OracleId { get; init; }
    public Guid CardId { get; init; }
    public string Name { get; init; }
    public string Rarity { get; init; }
    public string ManaCost { get; init; }
    public decimal Cmc { get; init; }
    public string TypeLine { get; init; }
    public string OracleText { get; init; }
    public string ReleasedAt { get; init; }
    public string ScryfallUri { get; init; }
    public string Lang { get; init; }
    public string Power { get; init; }
    public string Toughness { get; init; }
    public string[] Colors { get; init; } = Array.Empty<string>();
    public string[] ColorIdentity { get; init; } = Array.Empty<string>();
    public string[] Keywords { get; init; } = Array.Empty<string>();
    public ImageUrisModel ImageUris { get; init; }
}