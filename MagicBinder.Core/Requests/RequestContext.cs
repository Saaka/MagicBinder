namespace MagicBinder.Core.Requests;

public class RequestContext
{
    public bool IsAuthorized => User != null;
    public bool IsAdmin => IsAuthorized && (User?.IsAdmin ?? false);

    public AuthorizedUser? User { get; private set; }

    public RequestContext WithUser(AuthorizedUser user)
    {
        User = user;
        return this;
    }
}