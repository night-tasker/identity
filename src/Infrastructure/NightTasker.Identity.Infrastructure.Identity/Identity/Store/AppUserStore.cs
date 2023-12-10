using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Persistence;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Store;

/// <summary>
/// Стор для пользователей.
/// </summary>
public class AppUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<User, Role, ApplicationDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>(context, describer);