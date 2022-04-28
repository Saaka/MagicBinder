namespace MagicBinder.Core.Requests.Models;

public class AuthContextModel
{
    public bool IsAuthorized => User.Id != Guid.Empty;
    public bool IsAdmin => IsAuthorized && (User?.IsAdmin ?? false);
    public AuthorizedUser User { get; private init; } = new(Guid.Empty, false);

    public static AuthContextModel CreateAuthenticated(Guid id, bool isAdmin) => new()
    {
        User = new AuthorizedUser(id, isAdmin)
    };

    public static AuthContextModel CreateAnonymous() => new AuthContextModel();
}