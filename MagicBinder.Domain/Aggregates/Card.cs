using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Card
{
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Rarity { get; set; }
    public decimal Cmc { get; set; }
    public string ManaCost { get; set; } = string.Empty;
    public string TypeLine { get; set; } = string.Empty;
    public string OracleText { get; set; } = string.Empty;
    public string Power { get; set; } = string.Empty;
    public string Toughness { get; set; } = string.Empty;
    public string[] Colors { get; set; } = Array.Empty<string>();
    public string[] ColorIdentity { get; set; } = Array.Empty<string>();
    public string[] Keywords { get; set; } = Array.Empty<string>();
    public string[] Games { get; set; } = Array.Empty<string>();

    public CardPrinting LatestPrinting { get; set; } = new();
    public ICollection<CardPrinting> CardPrintings { get; set; } = new List<CardPrinting>();
}