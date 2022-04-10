namespace MagicBinder.Core.Exceptions;

public class InvalidPagedRequestException : ArgumentException
{
    public string Parameter { get; }

    public InvalidPagedRequestException(string parameter) : base($"Invalid parameter {parameter} value")
    {
        Parameter = parameter;
    }
}