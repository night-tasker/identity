using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Store;

/// <summary>
/// Стор для ролей.
/// </summary>
public class RoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<Role, ApplicationDbContext, Guid, UserRole, RoleClaim>(context, describer);