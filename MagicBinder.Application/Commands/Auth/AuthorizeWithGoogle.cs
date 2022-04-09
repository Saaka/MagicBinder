using MagicBinder.Application.Mappers;
using MagicBinder.Application.Models.Auth;
using MagicBinder.Infrastructure.Integrations.IdentityIssuer;
using MagicBinder.Infrastructure.Repositories;
using MediatR;

namespace MagicBinder.Application.Commands.Auth;

public record AuthorizeWithGoogle(string GoogleToken) : IRequest<AuthorizationModel>;

public class AuthorizeWithGoogleHandler : IRequestHandler<AuthorizeWithGoogle, AuthorizationModel>
{
    private readonly UsersRepository _usersRepository;
    private readonly IdentityIssuerClient _identityIssuerClient;

    public AuthorizeWithGoogleHandler(UsersRepository usersRepository, IdentityIssuerClient identityIssuerClient)
    {
        _usersRepository = usersRepository;
        _identityIssuerClient = identityIssuerClient;
    }

    public async Task<AuthorizationModel> Handle(AuthorizeWithGoogle request, CancellationToken cancellationToken)
    {
        var authData = await _identityIssuerClient.Authorize(request.GoogleToken);
        var user = authData.User.MapToAggregate();
        await _usersRepository.UpsertAsync(user);

        return new AuthorizationModel(user.MapToModel(), authData.Token);
    }
}