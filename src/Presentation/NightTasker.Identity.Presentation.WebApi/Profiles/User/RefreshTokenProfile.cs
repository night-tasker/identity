using Mapster;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Presentation.WebApi.Requests.User;

namespace NightTasker.Identity.Presentation.WebApi.Profiles.User;

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