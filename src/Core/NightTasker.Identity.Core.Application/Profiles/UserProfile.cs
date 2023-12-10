using Mapster;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Application.Profiles;

/// <summary>
/// Профиль регистрации маппов для сущности пользователя.
/// </summary>
public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUserDto, User>()
            .ConstructUsing(dto => new User(dto.UserName));

        config.ForType<User, CurrentUserInfo>();
    }
}