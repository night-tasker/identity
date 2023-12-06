using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

/// <summary>
/// Пользователь.
/// </summary>
public class User : IdentityUser<Guid>, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    public User(string userName) : base(userName)
    {
        
    }
    
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}