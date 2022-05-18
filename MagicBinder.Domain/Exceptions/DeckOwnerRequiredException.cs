namespace MagicBinder.Domain.Exceptions;

public class DeckOwnerRequiredException : ArgumentException
{
    public DeckOwnerRequiredException() : base("Owner is required for deck")
    {
    }
}