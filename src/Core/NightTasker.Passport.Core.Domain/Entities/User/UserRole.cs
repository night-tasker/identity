using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

/// <summary>
/// Роль пользователя.
/// </summary>
public class UserRole : IdentityUserRole<Guid>, IEntity, IDateTimeOffsetModification
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}