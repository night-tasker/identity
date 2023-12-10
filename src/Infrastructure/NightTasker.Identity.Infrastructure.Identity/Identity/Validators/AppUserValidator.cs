using Microsoft.AspNetCore.Identity;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Validators;

/// <summary>
/// Валидатор для пользователей. 
/// </summary>
public class AppUserValidator(IdentityErrorDescriber errors) : UserValidator<User>(errors)
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);

        return result;
    }
}