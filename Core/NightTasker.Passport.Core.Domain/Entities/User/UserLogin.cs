using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

public class UserLogin : IdentityUserLogin<Guid>, IDateTimeOffsetModification
{
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}