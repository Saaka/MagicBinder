namespace MagicBinder.Domain.Aggregates.Entities;

public class DeckCardCategory
{
    public string Name { get; set; } = string.Empty;
    public bool IsInDeck { get; set; }
}