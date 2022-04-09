using MagicBinder.Application.Models.Auth;
using MediatR;

namespace MagicBinder.Application.Commands.Auth;

public record AuthorizeWithGoogle(string GoogleToken) : IRequest<AuthorizationModel>;

public class AuthorizeWithGoogleHandler : IRequestHandler<AuthorizeWithGoogle, AuthorizationModel>
{
    public async Task<AuthorizationModel> Handle(AuthorizeWithGoogle request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}