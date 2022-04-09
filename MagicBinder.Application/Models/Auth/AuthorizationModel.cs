using MagicBinder.Application.Models.Users;

namespace MagicBinder.Application.Models.Auth;

public record AuthorizationModel(UserModel User, string Token);