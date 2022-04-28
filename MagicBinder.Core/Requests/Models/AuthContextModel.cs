namespace MagicBinder.Core.Requests.Models;

public class AuthContextModel
{
    public bool IsAuthorized => User != null;
    public bool IsAdmin => IsAuthorized && (User?.IsAdmin ?? false);
    public AuthorizedUser? User { get; private init; }

    public static AuthContextModel CreateAuthenticated(Guid id, bool isAdmin) => new()
    {
        User = new AuthorizedUser(id, isAdmin)
    };

    public static AuthContextModel CreateAnonymous() => new AuthContextModel();
}