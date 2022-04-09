namespace MagicBinder.Infrastructure.Integrations.IdentityIssuer.Models;

public record IdentityAuthorizationModel
{
    public string Token { get; init; }
    public IdentityUserModel User { get; init; }
}