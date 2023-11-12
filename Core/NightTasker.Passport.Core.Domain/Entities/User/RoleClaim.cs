using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

/// <summary>
/// Клейм роли.
/// </summary>
public class RoleClaim : IdentityRoleClaim<Guid>, IDateTimeOffsetModification
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}