using MagicBinder.Application.Queries.Cards;
using MagicBinder.Infrastructure.Repositories.Models;

namespace MagicBinder.Application.Mappers;

public static class QueriesMapper
{
    public static CardsListQueryParams MapToQueryParams(this GetCardsInfoQuery query) => new()
    {
        Name = query.Filters.Name,
        OracleText = query.Filters.OracleText,
        TypeLine = query.Filters.TypeLine,
        PageNumber = query.PageNumber,
        PageSize = query.PageSize
    };
}