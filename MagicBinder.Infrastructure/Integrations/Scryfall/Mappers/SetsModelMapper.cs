using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Infrastructure.Integrations.Scryfall.Mappers;

public static class SetsModelMapper
{
    public static Dictionary<string, string> GetFullSetsMapping() =>
        SetsImportMapping
            .Concat(SetsMapping)
            .ToDictionary(x => x.Key, v => v.Value);

    private static Dictionary<string, string> SetsImportMapping { get; } = new()
    {
        { nameof(SetsImportModel.Sets), "data" },
    };

    private static Dictionary<string, string> SetsMapping { get; } = new()
    {
        { nameof(SetModel.SetId), "id" },
        { nameof(SetModel.Code), "code" },
        { nameof(SetModel.Name), "name" },
        { nameof(SetModel.ScryfalUri), "scryfall_uri" },
        { nameof(SetModel.RealeasedAt), "released_at" },
        { nameof(SetModel.SetType), "set_type" },
        { nameof(SetModel.CardCount), "card_count" },
        { nameof(SetModel.Icon), "icon_svg_uri" },
    };
}