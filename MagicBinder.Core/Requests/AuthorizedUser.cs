namespace MagicBinder.Core.Requests;

public record AuthorizedUser(Guid UserGuid, bool IsAdmin);