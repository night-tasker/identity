using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Application.ApplicationContracts.Persistence;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public interface IUserRepository : IRepository<User, Guid>
{
    /// <summary>
    /// Получить <see cref="User"/> по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<User?> TryGetById(Guid id, CancellationToken cancellationToken);
}