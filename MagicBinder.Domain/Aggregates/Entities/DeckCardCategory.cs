namespace MagicBinder.Domain.Aggregates.Entities;

public class DeckCardCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsInDeck { get; set; }
}