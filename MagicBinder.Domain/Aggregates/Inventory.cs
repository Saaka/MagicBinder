using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Inventory
{
    public Guid OracleId { get; set; }
    public Guid UserId { get; set; }
    public string CardName { get; set; } = string.Empty;
    public List<InventoryPrinting> Printings { get; set; } = new();
}