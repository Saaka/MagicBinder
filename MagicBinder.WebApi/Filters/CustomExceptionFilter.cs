using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagicBinder.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilter: ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger _logger;

    public CustomExceptionFilter(IWebHostEnvironment env, ILogger<CustomExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        context.HttpContext.Response.ContentType = "application/json";
        _logger.LogError(exception, exception.Message);

        switch (exception)
        {
            default:
                HandleApplicationExceptions(context, exception);
                break;
        }
    }

    private void HandleApplicationExceptions(ExceptionContext context, Exception exception)
    {
        var code = exception switch
        {
            ArgumentNullException _ => HttpStatusCode.BadRequest,
            ArgumentException _ => HttpStatusCode.BadRequest,
            InvalidOperationException _ => HttpStatusCode.Forbidden,
            UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        context.HttpContext.Response.StatusCode = (int) code;
        context.Result = new JsonResult(new
        {
            Error = code.ToString(),
            Trace = GetTrace(context)
        });
    }

    private string? GetTrace(ExceptionContext context)
        => !_env.IsProduction() ? context.Exception.StackTrace : string.Empty;
}