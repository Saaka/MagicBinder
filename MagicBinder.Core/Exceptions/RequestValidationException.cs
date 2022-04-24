using FluentValidation.Results;

namespace MagicBinder.Core.Exceptions;

public class RequestValidationException : InvalidOperationException
{
    public RequestValidationException()
        : base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
        Errors = new List<string>();
    }

    public RequestValidationException(List<ValidationFailure> failures)
        : this()
    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }

        Errors = Failures.Select(x => x.Key).ToList();
    }

    public IList<string> Errors { get; }

    public IDictionary<string, string[]> Failures { get; }
}