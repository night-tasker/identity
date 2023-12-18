using Microsoft.AspNetCore.Identity;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Contracts;

public interface IAppUserManager
{
    Task<IdentityResult> AddPasswordAsync(User user, string password);
    
    Task<bool> CheckPasswordAsync(User user, string password);

    Task<IdentityResult> CreateAsync(User user);

    Task<User?> FindByNameAsync(string userName);
    
    IQueryable<User> Users { get; }
}