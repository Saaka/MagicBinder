using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Models.Decks;

public record DeckInfoModel
{
    public Guid DeckId { get; set; }
    public string Name { get; set; } = string.Empty;
    public FormatType Format { get; set; }
    public GameType GameType { get; set; }
}