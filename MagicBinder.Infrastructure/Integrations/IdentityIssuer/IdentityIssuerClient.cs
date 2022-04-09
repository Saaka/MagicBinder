using MagicBinder.Infrastructure.Configurations;
using MagicBinder.Infrastructure.Exceptions;
using MagicBinder.Infrastructure.Integrations.IdentityIssuer.Models;
using RestSharp;

namespace MagicBinder.Infrastructure.Integrations.IdentityIssuer;

public class IdentityIssuerClient
{
    private readonly RestClient _client;

    public IdentityIssuerClient(AuthConfig authConfig)
    {
        _client = new RestClient(authConfig.IdentityIssuerUrl)
            .AddDefaultHeader("X-Tenant-Code", authConfig.AppCode);
    }

    public virtual async Task<IdentityAuthorizationModel> Authorize(string googleToken)
    {
        var request = new RestRequest("api/auth/google", Method.Post)
            .AddJsonBody(new { GoogleToken = googleToken });

        var response = await _client.ExecuteAsync<IdentityAuthorizationModel>(request);

        return response is { IsSuccessful: true } ? response.Data : throw new ExternalProviderException(GetErrorMessage(response));
    }

    private static string GetErrorMessage(RestResponse<IdentityAuthorizationModel>? response)
    {
        return response == null ? nameof(IdentityIssuerClient) : response.ErrorMessage ?? response.StatusCode.ToString();
    }
}