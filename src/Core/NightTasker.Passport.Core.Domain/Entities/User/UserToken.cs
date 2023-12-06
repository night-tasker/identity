using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

/// <summary>
/// Токен пользователя.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IEntity, ICreatedDateTimeOffset
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}