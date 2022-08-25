namespace MagicBinder.Infrastructure.Repositories.Models;

public record UserInventoryQueryResult
{
    public Guid? CardId { get; set; }
    public Guid OracleId { get; set; }
    public string CardName { get; set; }
    public int Count { get; set; }
    public bool IsFoil { get; set; }
    public string Set { get; set; }
    public string SetName { get; set; }
    public string Image { get; set; }
}