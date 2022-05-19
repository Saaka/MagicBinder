using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Models.Decks;

public record DeckInfoModel
{
    public Guid DeckId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Format { get; init; } = string.Empty;
    public string GameType { get; init; } = string.Empty;
}