﻿using MagicBinder.Application.Queries.Cards;
using MagicBinder.Application.Queries.Decks;
using MagicBinder.Application.Queries.Inventories;
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

    public static UserDecksQueryParams MapToQueryParams(this GetUserDecksList query, Guid userId) => new()
    {
        OwnerId = userId,
        Name = query.Name,
        PageSize = query.PageSize,
        PageNumber = query.PageNumber
    };

    public static UserInventoryQueryParams MapToQueryParams(this GetUserInventory query, Guid userId) => new()
    {
        UserId = userId,
        CardName = query.CardName,
        SetName = query.SetName,
        PageNumber = query.PageNumber,
        PageSize = query.PageSize
    };
}