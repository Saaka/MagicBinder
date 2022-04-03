using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class CardPrinting
{
    public Guid CardId { get; set; }
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ReleasedAt { get; set; } = string.Empty;
    public string ScryfallUri { get; set; } = string.Empty;
    public string OracleText { get; set; } = string.Empty;
    public Guid SetId { get; set; }
    public string Set { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string CollectorNumber { get; set; }
    public string FlavorText { get; set; }
    public string Artist { get; set; }

    public CardImages? CardImages { get; set; }
}