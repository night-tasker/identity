using NightTasker.Passport.Application.Models.Jwt;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Jwt;

/// <summary>
/// Сервис для работы с JWT-токенами.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Генерация токена по данным пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT-Токен.</returns>
    Task<JwtAccessToken> GenerateAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление JWT-токена.
    /// </summary>
    /// <param name="refreshTokenId">Рефреш-токен</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновлённый JWT-токен.</returns>
    Task<JwtAccessToken> RefreshToken(Guid refreshTokenId, CancellationToken cancellationToken);
}