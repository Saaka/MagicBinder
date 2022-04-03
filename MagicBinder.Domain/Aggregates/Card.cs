using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Card
{
    public Guid CardId { get; set; }
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;

    public CardImages? CardImages { get; set; }
}