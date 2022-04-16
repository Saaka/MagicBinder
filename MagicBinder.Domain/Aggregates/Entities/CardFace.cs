using MagicBinder.Domain.Enums;

namespace MagicBinder.Domain.Aggregates.Entities;

public class CardFace
{
    public string Name { get; init; }
    public LayoutType Layout { get; set; }
    public string ManaCost { get; init; }
    public string Cmc { get; init; }
    public string FlavorText { get; init; }
    public string TypeLine { get; init; }
    public string OracleText { get; init; }
    public string[] Colors { get; init; } = Array.Empty<string>();
    public string Power { get; init; }
    public string Toughness { get; init; }
    public string Loyalty { get; init; }
    public string Artist { get; init; }
    public CardImages? CardImages { get; set; }
}