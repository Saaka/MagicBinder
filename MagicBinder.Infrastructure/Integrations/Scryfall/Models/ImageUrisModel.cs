namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record ImageUrisModel
{
    public string Small { get; init; }
    public string Normal { get; init; }
    public string Large { get; init; }
    public string PngRounded { get; init; }
    public string Art { get; init; }
}