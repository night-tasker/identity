using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

public class UserRefreshToken : IEntityWithId<Guid>, ICreatedDateTimeOffset
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User unique identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Related user.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Field for check if refresh token is valid. 
    /// </summary>
    public bool IsValid { get; set; }

    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}