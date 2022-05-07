using MagicBinder.Domain.Enums;

namespace MagicBinder.Domain.Aggregates.Entities;

public class CardPart
{
    public Guid CardId { get; set; }
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TypeLine { get; set; } = string.Empty;
    public PartComponentType Component { get; set; }
}