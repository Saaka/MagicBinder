namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record CardFaceModel
{
    public string Name { get; init; }
    public string ManaCost { get; init; }
    public string Cmc { get; init; }
    public string FlavorText { get; init; }
    public string Layout { get; init; }
    public string TypeLine { get; init; }
    public string OracleText { get; init; }
    public string[] Colors { get; init; } = Array.Empty<string>();
    public string Power { get; init; }
    public string Toughness { get; init; }
    public string Loyalty { get; init; }
    public string Artist { get; init; }
    public ImageUrisModel ImageUris { get; init; }
}