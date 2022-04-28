using MagicBinder.Application.Queries.Cards;
using MagicBinder.Infrastructure.Repositories.Models;

namespace MagicBinder.Application.Mappers;

public static class QueriesMapper
{
    public static CardsListQueryParams MapToQueryParams(this GetCardsSimpleList query) => new()
    {
        Name = query.Name,
        OracleText = query.OracleText,
        TypeLine = query.TypeLine,
        PageNumber = query.PageNumber,
        PageSize = query.PageSize
    };
}