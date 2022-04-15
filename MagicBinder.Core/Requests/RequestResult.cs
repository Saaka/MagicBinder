using MagicBinder.Core.Requests.Models;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Core.Requests;

public class RequestResult
{
    public bool IsSuccess { get; }
    public RequestErrorData? ErrorData { get; }

    public RequestResult() => IsSuccess = true;

    public RequestResult(RequestErrorData errorData) => (IsSuccess, ErrorData) = (false, errorData);

    public RequestResult<TResult> Success<TResult>(TResult data) => new(data);
    public RequestResult<TResult> Error<TResult>(ErrorCode errorCode, string errorDetails) => new RequestResult<TResult>(new RequestErrorData(errorCode, errorDetails));
}

public class RequestResult<TResult> : RequestResult
{
    public TResult? Data { get; }

    public RequestResult(TResult data) => Data = data;

    public RequestResult(RequestErrorData errorData) : base(errorData)
    {
    }
}