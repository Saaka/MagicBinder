namespace MagicBinder.Core.Requests.Models;

public record AuthorizedUser(Guid UserId, bool IsAdmin);