using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Infrastructure.Identity.Identity.Managers;

/// <summary>
/// Менеджер для работы с ролями.
/// </summary>
internal class AppRoleManager(IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<Role>> logger)
    : RoleManager<Role>(store, roleValidators, keyNormalizer, errors, logger);