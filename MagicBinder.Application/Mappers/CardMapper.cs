using MagicBinder.Application.Models.Cards;
using MagicBinder.Domain.Aggregates;

namespace MagicBinder.Application.Mappers;

public static class CardMapper
{
    public static CardInfoModel MapToCardInfo(Card card) => new()
    {
        OracleId = card.OracleId,
        Name = card.Name,
        Image = card.LatestPrinting.CardImages?.Normal ?? string.Empty
    };
}