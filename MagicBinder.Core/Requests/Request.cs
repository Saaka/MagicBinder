using MediatR;

namespace MagicBinder.Core.Requests;

public abstract record Request<TResult> : IRequest<RequestResult<TResult>>, IRequestBase
{
    public Guid RequestGuid { get; } = Guid.NewGuid();
    public RequestResult<TResult> Success(TResult data) => new(data);
}

public abstract record Request : Request<Guid>
{
    public RequestResult<Guid> Success() => new(RequestGuid);
}