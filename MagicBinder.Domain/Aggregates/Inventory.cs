using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Inventory
{
    public InventoryKey Key { get; }
    public string CardName { get; }
    public int TotalCount { get; private set; }
    public List<InventoryPrinting> Printings { get; set; } = new();

    public Inventory(Guid oracleId, Guid userId, string cardName, List<InventoryPrinting> printings)
    {
        Key = new InventoryKey(oracleId, userId);
        Printings = printings;
        CardName = cardName;
        
        TotalCount = printings.Count;
    }

    private Inventory()
    {
    }

    public Inventory AddPrinting(InventoryPrinting toAdd)
    {
        var existingPrinting = GetExistingPrinting(toAdd);
        if (existingPrinting == null)
        {
            Printings.Add(toAdd);
            TotalCount += toAdd.Count;
            return this;
        }

        existingPrinting.Count += toAdd.Count;
        TotalCount += existingPrinting.Count;
        return this;
    }

    private InventoryPrinting? GetExistingPrinting(InventoryPrinting model)
        => Printings.FirstOrDefault(x => x.CardId == model.CardId && x.IsFoil == model.IsFoil);
}

public record InventoryKey(Guid OracleId, Guid UserId);