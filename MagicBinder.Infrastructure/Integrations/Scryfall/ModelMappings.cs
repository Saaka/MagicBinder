using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Infrastructure.Integrations.Scryfall;

public static class ModelMappings
{
    public static Dictionary<string, string> GetFullCardMapping() =>
        CardsMapping
            .Concat(ImageUrisMapping)
            .Concat(LegalitiesMapping)
            .Concat(CardFacesMapping)
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
        { nameof(CardModel.Loyalty), "loyalty" },
        { nameof(CardModel.Colors), "colors" },
        { nameof(CardModel.ColorIdentity), "color_identity" },
        { nameof(CardModel.ProducedMana), "produced_mana" },
        { nameof(CardModel.Keywords), "keywords" },
        { nameof(CardModel.Games), "games" },
        { nameof(CardModel.ImageUris), "image_uris" },
        { nameof(CardModel.Set), "set" },
        { nameof(CardModel.SetId), "set_id" },
        { nameof(CardModel.SetName), "set_name" },
        { nameof(CardModel.SetType), "set_type" },
        { nameof(CardModel.Layout), "layout" },
        { nameof(CardModel.CollectorNumber), "collector_number" },
        { nameof(CardModel.FlavorText), "flavor_text" },
        { nameof(CardModel.Artist), "artist" },
        { nameof(CardModel.Oversized), "oversized" },
        { nameof(CardModel.Legalities), "legalities" },
        { nameof(CardModel.CardFaces), "card_faces" },
    };

    public static Dictionary<string, string> ImageUrisMapping { get; } = new()
    {
        { nameof(ImageUrisModel.Small), "small" },
        { nameof(ImageUrisModel.Normal), "normal" },
        { nameof(ImageUrisModel.Large), "large" },
        { nameof(ImageUrisModel.Art), "art_crop" },
        { nameof(ImageUrisModel.PngRounded), "png" },
    };

    public static Dictionary<string, string> LegalitiesMapping { get; } = new()
    {
        { nameof(LegalitiesModel.Alchemy), "alchemy" },
        { nameof(LegalitiesModel.Commander), "commander" },
        { nameof(LegalitiesModel.Historic), "historic" },
        { nameof(LegalitiesModel.Modern), "modern" },
        { nameof(LegalitiesModel.Pauper), "pauper" },
        { nameof(LegalitiesModel.Pioneer), "pioneer" },
        { nameof(LegalitiesModel.Standard), "standard" },
    };

    public static Dictionary<string, string> CardFacesMapping { get; } = new()
    {
        { nameof(CardFaceModel.Artist), "artist" },
        { nameof(CardFaceModel.Cmc), "cmc" },
        { nameof(CardFaceModel.Colors), "colors" },
        { nameof(CardFaceModel.FlavorText), "flavor_text" },
        { nameof(CardFaceModel.Layout), "layout" },
        { nameof(CardFaceModel.Loyalty), "loyalty" },
        { nameof(CardFaceModel.ManaCost), "mana_cost" },
        { nameof(CardFaceModel.Name), "name" },
        { nameof(CardFaceModel.Power), "power" },
        { nameof(CardFaceModel.Toughness), "toughness" },
        { nameof(CardFaceModel.ImageUris), "image_uris" },
        { nameof(CardFaceModel.OracleText), "oracle_text" },
        { nameof(CardFaceModel.TypeLine), "type_line" },
    };
}