namespace MagicBinder.Infrastructure.Integrations.IdentityIssuer.Models;

public record IdentityUserModel
{
    public Guid UserGuid { get; init; }
    public string DisplayName { get; init; }
    public string Email { get; init; }
    public string ImageUrl { get; init; }
    public bool IsAdmin { get; init; }
}