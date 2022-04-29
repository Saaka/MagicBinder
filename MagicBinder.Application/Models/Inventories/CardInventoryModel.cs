namespace MagicBinder.Application.Models.Inventories;

public class CardInventoryModel
{
    public Guid OracleId { get; set; }
    public List<InventoryPrintingModel> Printings { get; set; }

    public class InventoryPrintingModel
    {
        public Guid? CardId { get; set; }
        public int Count { get; set; }
        public bool IsFoil { get; set; }
    }
}