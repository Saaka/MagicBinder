namespace MagicBinder.Application.Models.Cards;

public record CardImagesModel
{
    public string Normal { get; init; } = string.Empty;
    public string Large { get; init; } = string.Empty;
    public string Art { get; init; } = string.Empty;
    public string PngRounded { get; init; } = string.Empty;
}