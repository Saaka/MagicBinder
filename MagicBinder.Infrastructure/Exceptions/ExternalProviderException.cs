namespace MagicBinder.Infrastructure.Exceptions;

public class ExternalProviderException : Exception
{
    public string Details { get; private set; }
        
    public ExternalProviderException(string message, string details = "")
        : base(message)
    {
        Details = details;
    }
}