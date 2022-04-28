using MagicBinder.Core.Requests.Models;

namespace MagicBinder.Core.Requests;

public interface IRequestContextService
{
    AuthContextModel CurrentContext { get; }
}

public class RequestContextService : IRequestContextService
{
    public RequestContextService()
    {
        CurrentContext = AuthContextModel.CreateAnonymous();
    }

    public virtual void SetUser(Guid userId, bool isAdmin) => CurrentContext = AuthContextModel.CreateAuthenticated(userId, isAdmin);
    
    public virtual AuthContextModel CurrentContext { get; private set; }
}