namespace MagicBinder.Domain.Exceptions;

public class AggregateIdRequiredException : ArgumentException
{
    public string AggregateName { get; }
    public AggregateIdRequiredException(string aggregateName) : base("Id required for `{Aggregate}` aggregate", aggregateName)
    {
        AggregateName = aggregateName;
    }
}