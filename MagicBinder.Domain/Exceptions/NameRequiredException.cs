namespace MagicBinder.Domain.Exceptions;

public class NameRequiredException : ArgumentException
{
    public string Aggregate { get; }

    public NameRequiredException(string aggregate) :base("Name required for {Aggregate}", aggregate)
    {
        Aggregate = aggregate;
    }
}