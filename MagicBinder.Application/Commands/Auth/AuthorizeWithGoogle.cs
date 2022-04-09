using MagicBinder.Application.Models.Auth;
using MagicBinder.Infrastructure.Repositories;
using MediatR;

namespace MagicBinder.Application.Commands.Auth;

public record AuthorizeWithGoogle(string GoogleToken) : IRequest<AuthorizationModel>;

public class AuthorizeWithGoogleHandler : IRequestHandler<AuthorizeWithGoogle, AuthorizationModel>
{
    private readonly UsersRepository _usersRepository;

    public AuthorizeWithGoogleHandler(UsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<AuthorizationModel> Handle(AuthorizeWithGoogle request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}