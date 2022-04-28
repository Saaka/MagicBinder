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

    public virtual void SetUser(Guid userId, bool isAdmin) => User = AuthContextModel.CreateAuthenticated(userId, isAdmin);
    
    public virtual AuthContextModel User { get; private set; }
}