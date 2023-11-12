using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Validators;

/// <summary>
/// Валидатор для пользователей. 
/// </summary>
public class AppUserValidator : UserValidator<User>
{
    public AppUserValidator(IdentityErrorDescriber errors) : base(errors) 
    {
        
    }
    
    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);

        return result;
    }
}