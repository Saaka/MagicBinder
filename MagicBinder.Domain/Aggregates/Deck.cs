using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MagicBinder.Domain.Exceptions;

namespace MagicBinder.Domain.Aggregates;

public class Deck
{
    public Guid DeckId { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public FormatType Format { get; private set; }
    public GameType GameType { get; private set; }

    public List<DeckCardCategory> Categories { get; private set; } = new();
    public List<DeckCard> DeckCards { get; private set; } = new();

    public Deck(Guid deckId, Guid ownerId, string name, FormatType format, GameType gameType, List<DeckCardCategory> Categories)
    {
        DeckId = deckId;
        OwnerId = ownerId;
        Name = name;
        Format = format;
        GameType = gameType;

        ValidateCreation();
    }

    public Deck UpdateDeckList(List<DeckCard> deckCards)
    {
        DeckCards = deckCards;

        return this;
    }

    public Deck UpdateSettings(string name, FormatType formatType, GameType gameType)
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
        if (OwnerId == Guid.Empty) throw new DeckOwnerRequiredException();
        ValidateName(Name);
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new NameRequiredException(nameof(Deck));
    }
}