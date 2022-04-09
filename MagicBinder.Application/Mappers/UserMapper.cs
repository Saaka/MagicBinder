using MagicBinder.Application.Models.Users;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Infrastructure.Integrations.IdentityIssuer.Models;

namespace MagicBinder.Application.Mappers;

public static class UserMapper
{
    public static User MapToAggregate(this IdentityUserModel model)
        => new User(model.UserGuid, model.DisplayName, model.Email, model.ImageUrl, model.IsAdmin);

    public static UserModel MapToModel(this User user)
        => new()
        {
            UserGuid = user.UserGuid,
            Email = user.Email,
            DisplayName = user.DisplayName,
            ImageUrl = user.ImageUrl,
            IsAdmin = user.IsAdmin
        };
}