using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Inventory
{
    public InventoryKey Key { get; set; }
    public string CardName { get; set; } = string.Empty;
    public List<InventoryPrinting> Printings { get; set; } = new();
}

public class InventoryKey
{
    public Guid OracleId { get; set; }
    public Guid UserId { get; set; }
}