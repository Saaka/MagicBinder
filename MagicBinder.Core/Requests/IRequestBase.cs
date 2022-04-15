namespace MagicBinder.Core.Requests;

public interface IRequestBase
{
    Guid RequestGuid { get; }
    RequestContext Context { get; }
    void SetContext(RequestContext context);
}