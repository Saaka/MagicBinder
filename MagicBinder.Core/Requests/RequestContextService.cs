using MagicBinder.Core.Requests.Models;

namespace MagicBinder.Core.Requests;

public interface IRequestContextService
{
    AuthContextModel User { get; }
}

public class RequestContextService : IRequestContextService
{
    public RequestContextService()
    {
        User = AuthContextModel.CreateAnonymous();
    }

    public virtual void SetUser(Guid userGuid, bool isAdmin) => User = AuthContextModel.CreateAuthenticated(userGuid, isAdmin);
    
    public virtual AuthContextModel User { get; private set; }
}