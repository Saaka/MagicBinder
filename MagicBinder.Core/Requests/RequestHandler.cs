﻿using MediatR;

namespace MagicBinder.Core.Requests;

public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, RequestResult<TResult>>
    where TRequest : Request<TResult>
{
    public abstract Task<RequestResult<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
}