using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Auth;
using MagicBinder.Core.Requests;
using MagicBinder.Infrastructure.Integrations.IdentityIssuer;
using MagicBinder.Infrastructure.Repositories;

namespace MagicBinder.Application.Commands.Auth;

public class AuthorizeWithGoogle : Request<AuthorizationModel>
{
    public string GoogleToken { get; set; }
}

public class AuthorizeWithGoogleHandler : RequestHandler<AuthorizeWithGoogle, AuthorizationModel>
{
    private readonly UsersRepository _usersRepository;
    private readonly IdentityIssuerClient _identityIssuerClient;

    public AuthorizeWithGoogleHandler(UsersRepository usersRepository, IdentityIssuerClient identityIssuerClient)
    {
        _usersRepository = usersRepository;
        _identityIssuerClient = identityIssuerClient;
    }

    public override async Task<RequestResult<AuthorizationModel>> Handle(AuthorizeWithGoogle request, CancellationToken cancellationToken)
    {
        var authData = await _identityIssuerClient.Authorize(request.GoogleToken);
        var user = authData.User.MapToAggregate();
        await _usersRepository.UpsertAsync(user);

        return request.Success(new AuthorizationModel(user.MapToModel(), authData.Token));
    }
}