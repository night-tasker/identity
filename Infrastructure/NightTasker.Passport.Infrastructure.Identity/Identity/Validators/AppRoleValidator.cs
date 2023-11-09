﻿using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Validators;

public class AppRoleValidator : RoleValidator<Role>
{
    public AppRoleValidator(IdentityErrorDescriber errors) : base(errors) 
    {
        
    }
    
    public override async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
    {
        var result = await base.ValidateAsync(manager, role);

        return result;
    }
}