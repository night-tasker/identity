using NightTasker.Common.Core.Persistence;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Persistence.Contracts;

/// <summary>
/// Акссессор к базе данных.
/// </summary>
public interface IApplicationDbAccessor
{
    /// <summary>
    /// Набор записей <see cref="UserRefreshToken"/>.
    /// </summary>
    ApplicationDbSet<UserRefreshToken, Guid> UserRefreshTokens { get; }

    /// <summary>
    /// Набор записей <see cref="User"/>.
    /// </summary>
    ApplicationDbSet<User, Guid> Users { get; }

    /// <summary>
    /// Сохранять изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SaveChanges(CancellationToken cancellationToken);
}