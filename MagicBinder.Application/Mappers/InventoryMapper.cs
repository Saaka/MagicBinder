using MagicBinder.Application.Models.Inventories;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Infrastructure.Repositories.Models;

namespace MagicBinder.Application.Mappers;

public static class InventoryMapper
{
    public static UserInventoryModel MapToUserInventory(this UserInventoryQueryResult model) => new()
    {
        CardId = model.CardId,
        OracleId = model.OracleId,
        CardName = model.CardName,
        Count = model.Count,
        Set = model.Set,
        SetName = model.SetName,
        IsFoil = model.IsFoil,
        Image = model.Image
    };

    public static InventoryPrinting MapToInventoryPrinting(this CardPrinting printing, Guid? cardId, int count, bool isFoil) => new()
    {
        CardId = cardId,
        Count = count,
        IsFoil = isFoil,
        Set = printing.Set,
        SetName = printing.SetName,
        Language = printing.Lang,
        CollectorNumber = printing.CollectorNumber,
        Image = printing.CardImages?.Normal ?? string.Empty
    };

    public static CardInventoryModel MapToModel(this Inventory? inventory, Guid oracleId)
        => inventory == null
            ? new CardInventoryModel { OracleId = oracleId }
            : new CardInventoryModel
            {
                OracleId = inventory.Key.OracleId,
                Printings = inventory.Printings.Select(MapToPrintingModel).ToList()
            };

    private static CardInventoryModel.InventoryPrintingModel MapToPrintingModel(InventoryPrinting printing) => new()
    {
        CardId = printing.CardId,
        Count = printing.Count,
        IsFoil = printing.IsFoil,
        Set = printing.Set,
        SetName = printing.SetName,
        Image = printing.Image
    };
}