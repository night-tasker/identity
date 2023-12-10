using NightTasker.Identity.Domain.Entities.Common;

namespace NightTasker.Identity.Domain.Entities.User;

/// <summary>
/// Рефреш-токен пользователя.
/// </summary>
public class UserRefreshToken : IEntityWithId<Guid>, ICreatedDateTimeOffset
{
    /// <summary>
    /// ИД рефреш-токена пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ИД пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public User? User { get; set; } = null!;

    /// <summary>
    /// Валидность рефреш-токена.
    /// </summary>
    public bool IsValid { get; set; }

    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}