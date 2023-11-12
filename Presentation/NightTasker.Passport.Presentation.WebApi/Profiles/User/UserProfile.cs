using Mapster;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Profiles.User;

/// <summary>
/// Профиль для маппов пользователя.
/// </summary>
public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<LoginUserRequest, LoginUserDto>();
        config.ForType<CreateUserRequest, CreateUserDto>();
    }
}