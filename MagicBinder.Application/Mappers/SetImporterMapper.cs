using MagicBinder.Domain.Aggregates;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;

namespace MagicBinder.Application.Mappers;

public static class SetImporterMapper
{
    public static Set MapToAggregate(this SetModel model) => new()
    {
        SetId = model.SetId,
        Code = model.Code,
        Name = model.Name,
        Icon = model.Icon,
        CardCount = model.CardCount,
        ReleasedAt = model.RealeasedAt,
        ScryfallUri = model.ScryfallUri,
        SetType = model.SetType
    };
}