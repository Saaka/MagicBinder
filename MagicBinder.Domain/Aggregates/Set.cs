namespace MagicBinder.Domain.Aggregates;

public class Set
{
    public Guid SetId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ScryfallUri { get; set; }
    public string ReleasedAt { get; set; }
    public int CardCount { get; set; }
    public string SetType { get; set; }
    public string Icon { get; set; }
}