using System.Security.Claims;
using MagicBinder.Core.Requests;
using MagicBinder.Domain.Constants;

namespace MagicBinder.WebApi.Middleware;

public class AuthenticatedRequestMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticatedRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, RequestContextService requestContextService)
    {
        if (HasUserContext(context))
        {
            var userId = GetUserIdFromContext(context);
            var isAdmin = IsAdmin(context);

            requestContextService.SetUser(userId, isAdmin);
        }

        await _next(context);
    }

    private static bool IsAdmin(HttpContext context) => context.User.IsInRole(UserRoles.Admin);

    private static Guid GetUserIdFromContext(HttpContext context)
    {
        var guid = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return guid == null ? Guid.Empty : new Guid(guid);
    }

    private static bool HasUserContext(HttpContext context)
        => context.User?.Claims != null && context.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);
}