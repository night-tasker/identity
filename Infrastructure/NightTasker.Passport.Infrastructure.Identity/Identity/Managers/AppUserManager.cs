﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Managers;

public class AppUserManager : UserManager<User>
{
    public AppUserManager(
        IUserStore<User> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<User> passwordHasher, 
        IEnumerable<IUserValidator<User>> userValidators, 
        IEnumerable<IPasswordValidator<User>> passwordValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }
}