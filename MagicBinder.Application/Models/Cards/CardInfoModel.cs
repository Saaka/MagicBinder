namespace MagicBinder.Application.Models.Cards;

public record CardInfoModel
{
    public Guid OracleId { get; init; }
    public string Name { get; init; }
    public string TypeLine { get; init; }
    public string Image { get; init; }
    public string Artist { get; init; }
}