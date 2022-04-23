namespace MagicBinder.Infrastructure.Integrations.Scryfall.Models;

public record CardModel
{
    public Guid OracleId { get; init; }
    public Guid CardId { get; init; }
    public string Name { get; init; }
    public string Rarity { get; init; }
    public string ManaCost { get; init; }
    public decimal Cmc { get; init; }
    public string TypeLine { get; init; }
    public string OracleText { get; init; }
    public string ReleasedAt { get; init; }
    public string ScryfallUri { get; init; }
    public string Lang { get; init; }
    public string Power { get; init; }
    public string Loyalty { get; init; }
    public string Toughness { get; init; }
    public Guid SetId { get; init; }
    public string Set { get; init; }
    public string SetName { get; init; }
    public string SetType { get; init; }
    public string Layout { get; init; }
    public string CollectorNumber { get; init; }
    public string FlavorText { get; init; }
    public string Artist { get; init; }
    public bool Oversized { get; init; }
    public string[] Colors { get; init; } = Array.Empty<string>();
    public string[] ColorIdentity { get; init; } = Array.Empty<string>();
    public string[] Keywords { get; init; } = Array.Empty<string>();
    public string[] Games { get; init; } = Array.Empty<string>();
    public string[] ProducedMana { get; init; } = Array.Empty<string>();
    public ImageUrisModel ImageUris { get; init; }
    public LegalitiesModel Legalities { get; init; }
    public CardFaceModel[] CardFaces { get; init; } = Array.Empty<CardFaceModel>();
}