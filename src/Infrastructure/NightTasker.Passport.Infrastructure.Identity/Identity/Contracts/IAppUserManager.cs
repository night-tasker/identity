using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Contracts;

public interface IAppUserManager
{
    Task<IdentityResult> AddPasswordAsync(User user, string password);
    
    Task<bool> CheckPasswordAsync(User user, string password);

    Task<IdentityResult> CreateAsync(User user);
    
    IQueryable<User> Users { get; }
}