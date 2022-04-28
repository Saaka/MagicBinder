using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Application.Mappers;

public static class InventoryMapper
{
    public static InventoryPrinting MapToInventoryPrinting(this CardPrinting printing, int count, bool isFoil) => new()
    {
        CardId = printing.CardId,
        Count = count,
        IsFoil = isFoil,
        Set = printing.Set,
        SetName = printing.SetName,
        Language = printing.Lang,
        CollectorNumber = printing.CollectorNumber,
        Image = printing.CardImages?.Normal ?? string.Empty
    };
}