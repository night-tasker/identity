using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Store;

/// <summary>
/// Стор для пользователей.
/// </summary>
public class AppUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<User, Role, ApplicationDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>(context, describer);