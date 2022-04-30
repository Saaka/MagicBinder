namespace MagicBinder.Application.Models.Inventories;

public class CardInventoryModel
{
    public Guid OracleId { get; set; }
    public List<InventoryPrintingModel> Printings { get; set; } = new();

    public class InventoryPrintingModel
    {
        public Guid? CardId { get; set; }
        public int Count { get; set; }
        public bool IsFoil { get; set; }
        public string Set { get; set; }
        public string SetName { get; set; }
        public string Image { get; set; }
    }
}