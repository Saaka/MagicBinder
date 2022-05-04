using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Domain.Exceptions;

namespace MagicBinder.Domain.Aggregates;

public class Deck
{
    public Guid DeckId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public FormatType Format { get; private set; }
    public GameType GameType { get; private set; }

    public List<DeckCard> DeckCards { get; private set; } = new();

    public Deck(Guid deckId, string name, FormatType format, GameType gameType)
    {
        DeckId = deckId;
        Name = name;
        Format = format;
        GameType = gameType;

        ValidateCreation();
    }

    public Deck Update(string name, FormatType formatType, GameType gameType)
    {
        ValidateName(name);
        Name = name;
        Format = formatType;
        GameType = gameType;

        return this;
    }

    private void ValidateCreation()
    {
        if (DeckId == Guid.Empty) throw new AggregateIdRequiredException(nameof(Deck));
        ValidateName(Name);
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new NameRequiredException(nameof(Deck));
    }
}