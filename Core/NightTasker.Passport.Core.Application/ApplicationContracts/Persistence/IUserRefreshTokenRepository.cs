using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Persistence;

public interface IUserRefreshTokenRepository
{
    Task<Guid> CreateToken(Guid userId, CancellationToken cancellationToken);

    Task<UserRefreshToken?> TryGetValidRefreshToken(Guid userId, CancellationToken cancellationToken);

    Task<User?> TryGetUserByRefreshToken(Guid refreshTokenId, CancellationToken cancellationToken);
}