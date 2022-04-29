namespace MagicBinder.Domain.Aggregates.Entities;

public class InventoryPrinting
{
    public Guid? CardId { get; set; }
    public int Count { get; set; }
    public string Set { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string CollectorNumber { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool IsFoil { get; set; }
    public string Image { get; set; }
}