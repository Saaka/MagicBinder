namespace MagicBinder.Domain.Aggregates.Entities;

public class UserCard
{
    public Guid CardId { get; set; }
    public int Count { get; set; }
    public string Set { get; set; }
    public string SetName { get; set; }
    public string CollectorNumber { get; set; }
    public string Language { get; set; }
    public bool IsFoil { get; set; }
    public string Image { get; set; }
}