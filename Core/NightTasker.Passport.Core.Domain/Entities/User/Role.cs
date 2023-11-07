using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

public class Role : IdentityRole<Guid>, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
 
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}