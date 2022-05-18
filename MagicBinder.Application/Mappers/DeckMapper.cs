using MagicBinder.Application.Commands.Decks;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Application.Mappers;

public static class DeckMapper
{
    public static Deck MapToAggregate(this CreateDeck model, Guid newGuid, Guid owner, IEnumerable<DeckCardCategory> defaultCategories)
        => new(newGuid, owner, model.Name, model.Format, model.GameType, defaultCategories.ToList());
}