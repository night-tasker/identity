using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Persistence;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Store;

/// <summary>
/// Стор для ролей.
/// </summary>
public class RoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<Role, ApplicationDbContext, Guid, UserRole, RoleClaim>(context, describer);