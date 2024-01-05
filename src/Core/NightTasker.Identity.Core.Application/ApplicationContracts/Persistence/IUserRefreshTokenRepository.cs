using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Application.ApplicationContracts.Persistence;

/// <summary>
/// Репозиторий рефреш-токенов пользователей.
/// </summary>
public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken, Guid>
{
    /// <summary>
    /// Создать рефреш-токен для пользователя.
    /// </summary>
    /// <param name="userId">ИД пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ИД рефреш-токена.</returns>
    Task<Guid> CreateToken(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Попробовать получить валидный рефреш-токен через ИД рефреш-токена
    /// </summary>
    /// <param name="refreshTokenId">ИД рефреш-токена.</param>
    /// <param name="trackChanges">Отслеживать изменения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Рефреш токен.</returns>
    Task<UserRefreshToken?> TryGetValidRefreshToken(
        Guid refreshTokenId, 
        bool trackChanges,
        CancellationToken cancellationToken);

    /// <summary>
    /// Попробовать получить пользователя через ИД рефреш-токена.
    /// </summary>
    /// <param name="refreshTokenId">ИД рефреш-токена.</param>
    /// <param name="trackChanges">Отслеживать изменения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<User?> TryGetUserByRefreshToken(
        Guid refreshTokenId, 
        bool trackChanges,
        CancellationToken cancellationToken);
}