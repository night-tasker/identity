using Mapster;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Presentation.WebApi.Requests.User;

namespace NightTasker.Identity.Presentation.WebApi.Profiles.User;

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