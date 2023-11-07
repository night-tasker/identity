using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.Common;

namespace NightTasker.Passport.Domain.Entities.User;

public class UserToken : IdentityUserToken<Guid>, IEntity, ICreatedDateTimeOffset
{
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}