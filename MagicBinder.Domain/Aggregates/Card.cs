using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Domain.Aggregates;

public class Card
{
    public Guid OracleId { get; set; }
    public LayoutType Layout { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Cmc { get; set; }
    public string ManaCost { get; set; } = string.Empty;
    public string Power { get; set; } = string.Empty;
    public string Toughness { get; set; } = string.Empty;
    public string Loyalty { get; set; } = string.Empty;
    public ColorType[] Colors { get; set; } = Array.Empty<ColorType>();
    public ColorType[] ColorIdentity { get; set; } = Array.Empty<ColorType>();
    public ColorType[] ProducedMana { get; set; } = Array.Empty<ColorType>();
    public string[] Keywords { get; set; } = Array.Empty<string>();
    public GameType[] Games { get; set; } = Array.Empty<GameType>();
    public FormatType[] LegalIn { get; set; } = Array.Empty<FormatType>();
    public CardType CardType { get; set; }

    public CardPrinting LatestPrinting { get; set; } = new();
    public List<CardPrinting> CardPrintings { get; set; } = new();
}