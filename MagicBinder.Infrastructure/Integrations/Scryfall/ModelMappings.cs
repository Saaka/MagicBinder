using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Infrastructure.Integrations.Scryfall;

public static class ModelMappings
{
    public static Dictionary<string, string> GetFullCardMapping() =>
        CardsMapping
            .Concat(ImageUrisMapping)
            .GroupBy(x => x.Key)
            .ToDictionary(x => x.Key, v => v.First().Value);

    public static Dictionary<string, string> CardsMapping { get; } = new()
    {
        { nameof(CardModel.CardId), "id" },
        { nameof(CardModel.OracleId), "oracle_id" },
        { nameof(CardModel.Name), "name" },
        { nameof(CardModel.ScryfallUri), "scryfall_uri" },
        { nameof(CardModel.Lang), "lang" },
        { nameof(CardModel.Cmc), "cmc" },
        { nameof(CardModel.ManaCost), "mana_cost" },
        { nameof(CardModel.TypeLine), "type_line" },
        { nameof(CardModel.OracleText), "oracle_text" },
        { nameof(CardModel.Rarity), "rarity" },
        { nameof(CardModel.ReleasedAt), "released_at" },
        { nameof(CardModel.Power), "power" },
        { nameof(CardModel.Toughness), "toughness" },
        { nameof(CardModel.Colors), "colors" },
        { nameof(CardModel.ColorIdentity), "color_identity" },
        { nameof(CardModel.Keywords), "keywords" },
        { nameof(CardModel.ImageUris), "image_uris" },
    };

    public static Dictionary<string, string> ImageUrisMapping { get; } = new()
    {
        { nameof(ImageUrisModel.Small), "small" },
        { nameof(ImageUrisModel.Normal), "normal" },
        { nameof(ImageUrisModel.Large), "large" },
        { nameof(ImageUrisModel.Art), "art_crop" },
        { nameof(ImageUrisModel.PngRounded), "png" },
    };
}