using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Managers;

internal class AppRoleManager : RoleManager<Role>
{
    public AppRoleManager(
        IRoleStore<Role> store, 
        IEnumerable<IRoleValidator<Role>> roleValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }
}