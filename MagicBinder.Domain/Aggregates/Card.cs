using MagicBinder.Domain.Aggregates.Entities;

namespace MagicBinder.Domain.Aggregates;

public class Card
{
    public Guid OracleId { get; set; }
    public Guid CardId { get; set; }
    public string Name { get; set; }
    public ImageUris ImageUris { get; set; }
}