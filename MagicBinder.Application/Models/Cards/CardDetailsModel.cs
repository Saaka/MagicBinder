using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Models.Cards;

public record CardDetailsModel
{
    public Guid OracleId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string TypeLine { get; init; } = string.Empty;
    public string OracleText { get; init; } = string.Empty;
    public ColorType[] Colors { get; init; } = Array.Empty<ColorType>();
    public CardImagesModel? Images { get; init; }
    public string ScryfallUri { get; init; } = string.Empty;
}