namespace MagicBinder.Core.Requests;

public class RequestResult
{
    public bool IsSuccess { get; }
    
    public RequestResult() => IsSuccess = true;
}

public class RequestResult<TResult> : RequestResult
{
    public TResult Data { get; }

    public RequestResult(TResult data) => Data = data;
}