using MagicBinder.Application.Models.Dictionaries;
using MagicBinder.Domain.Aggregates;

namespace MagicBinder.Application.Mappers;

public static class SetsMapper
{
    public static SetModel MapToModel(this Set set) => new()
    {
        SetId = set.SetId,
        Code = set.Code,
        Name = set.Name
    };
}