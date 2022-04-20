using MagicBinder.Domain.Enums;

namespace MagicBinder.Domain.Aggregates.Entities;

public class CardPrinting
{
    public Guid CardId { get; set; }
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ReleasedAt { get; set; } = string.Empty;
    public string ScryfallUri { get; set; } = string.Empty;
    public string TypeLine { get; set; } = string.Empty;
    public string OracleText { get; set; } = string.Empty;
    public Guid SetId { get; set; }
    public string Set { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string CollectorNumber { get; set; }
    public string FlavorText { get; set; }
    public string Artist { get; set; }
    public string Lang { get; set; }
    public GameType[] Games { get; set; } = Array.Empty<GameType>();
    public FormatType[] LegalIn { get; set; } = Array.Empty<FormatType>();

    public ICollection<CardFace> CardFaces { get; set; } = new List<CardFace>();
    public CardImages? CardImages { get; set; }
}