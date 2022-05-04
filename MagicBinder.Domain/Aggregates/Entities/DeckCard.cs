namespace MagicBinder.Domain.Aggregates.Entities;

public class DeckCard
{
    public Guid OracleId { get; set; }
    public Guid CardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}