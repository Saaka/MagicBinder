namespace MagicBinder.Infrastructure.Configurations;

public class AuthConfig
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string AppCode { get; set; }
    public string IdentityIssuerUrl { get; set; }
    public string AllowedOrigin{ get; set; }
}