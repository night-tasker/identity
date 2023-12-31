﻿using Microsoft.AspNetCore.Identity;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Validators;

/// <summary>
/// Валидатор для роли.
/// </summary>
public class AppRoleValidator(IdentityErrorDescriber errors) : RoleValidator<Role>(errors)
{
    public override async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
    {
        var result = await base.ValidateAsync(manager, role);

        return result;
    }
}