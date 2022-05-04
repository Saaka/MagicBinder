namespace MagicBinder.Domain.Aggregates;

public class Deck
{
    public Guid DeckId { get; set; }
    public string Name { get; set; } = string.Empty;
}