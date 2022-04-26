using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Inventory
{
    public Guid OracleId { get; set; }
    public Guid UserGuid { get; set; }
    public List<UserCard> Cards { get; set; } = new();
}