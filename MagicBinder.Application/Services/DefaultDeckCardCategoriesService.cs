using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Services;

public class DefaultDeckCardCategoriesService
{
    public IEnumerable<DeckCardCategory> GetDefaultCardCategories(FormatType formatType)
        => formatType switch
        {
            FormatType.Commander => CommanderCategories,
            _ => DefaultCategories
        };

    private static IEnumerable<DeckCardCategory> CommanderCategories =>
        new List<DeckCardCategory>
        {
            new() { Name = "Commander", IsInDeck = true, IsCommander = true},
            new() { Name = "Sideboard", IsInDeck = false },
            new() { Name = "InConsideration", IsInDeck = false },
        };

    private static IEnumerable<DeckCardCategory> DefaultCategories =>
        new List<DeckCardCategory>
        {
            new() { Name = "Sideboard", IsInDeck = false },
            new() { Name = "InConsideration", IsInDeck = false },
        };
}