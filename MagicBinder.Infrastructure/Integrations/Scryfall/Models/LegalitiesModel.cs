namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record LegalitiesModel
{
    public string Commander { get; init; }
    public string Standard { get; init; }
    public string Modern { get; init; }
    public string Pioneer { get; init; }
    public string Pauper { get; init; }
    public string Historic { get; init; }
    public string Alchemy { get; init; }

    public const string Legal = "legal";
    public const string NotLegal = "not_legal";
}