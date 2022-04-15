namespace MagicBinder.Core.Requests.Models;

public record AuthorizedUser(Guid UserGuid, bool IsAdmin);