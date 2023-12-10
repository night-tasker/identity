﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Identity.Identity.Contracts;

namespace NightTasker.Passport.Infrastructure.Identity.Identity.Managers;

/// <summary>
/// Менеджер для работы с пользователями.
/// </summary>
public class AppUserManager(IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger)
    : UserManager<User>(store, 
        optionsAccessor, 
        passwordHasher, 
        userValidators, 
        passwordValidators, 
        keyNormalizer,
        errors, 
        services, 
        logger), IAppUserManager;