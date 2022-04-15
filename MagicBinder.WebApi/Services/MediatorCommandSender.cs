using System.Net;
using MagicBinder.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MagicBinder.WebApi.Services;

public class MediatorCommandSender
{
    private readonly IMediator _mediator;

    public MediatorCommandSender(IMediator mediator)
    {
        _mediator = mediator;
    }

    public virtual async Task<ActionResult<TResponse>> SendCommand<TResponse>(Request<TResponse> command)
    {
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? new ObjectResult(result.Data) { StatusCode = (int)HttpStatusCode.OK }
            : new ObjectResult(result.ErrorData) { StatusCode = (int)HttpStatusCode.BadRequest };
    }

    public virtual async Task<ActionResult<TResponse>> SendQuery<TResponse>(Request<TResponse> command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess) new ObjectResult(result.ErrorData) { StatusCode = (int)HttpStatusCode.BadRequest };

        if (result.Data == null) return new NotFoundResult();

        return new ObjectResult(result.Data) { StatusCode = (int)HttpStatusCode.OK };
    }
}