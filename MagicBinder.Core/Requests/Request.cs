using MediatR;

namespace MagicBinder.Core.Requests;

public abstract class Request<TResult> : IRequest<RequestResult<TResult>>, IRequestBase
{
    public Guid RequestGuid { get; } = Guid.NewGuid();
    public RequestContext? Context { get; private set; }
    void IRequestBase.SetContext(RequestContext context) => Context = context;

    public RequestResult<TResult> Success(TResult data) => new(data);
}

public abstract class Request : Request<Guid>
{
    public RequestResult<Guid> Success() => new(RequestGuid);
}