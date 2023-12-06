using Mapster;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Profiles.User;

/// <summary>
/// Профиль маппов для рефреш-токена.
/// </summary>
public class RefreshTokenProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<RefreshTokenRequest, RefreshUserTokenDto>()
            .Map(dest => dest.RefreshTokenId, src => src.RefreshToken);
    }
}