using NightTasker.Passport.Application.Models.Jwt;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Jwt;

public interface IJwtService
{
    Task<JwtAccessToken> GenerateAsync(User user, CancellationToken cancellationToken);

    Task<JwtAccessToken?> RefreshToken(Guid refreshTokenId, CancellationToken cancellationToken);
}