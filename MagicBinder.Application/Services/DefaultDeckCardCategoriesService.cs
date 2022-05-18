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
            new DeckCardCategory { Name = "Commander", IsInDeck = true },
            new DeckCardCategory { Name = "Sideboard", IsInDeck = false },
            new DeckCardCategory { Name = "InConsideration", IsInDeck = false },
        };

    private static IEnumerable<DeckCardCategory> DefaultCategories =>
        new List<DeckCardCategory>
        {
            new DeckCardCategory { Name = "Mainboard", IsInDeck = true },
            new DeckCardCategory { Name = "Sideboard", IsInDeck = false },
            new DeckCardCategory { Name = "InConsideration", IsInDeck = false },
        };
}