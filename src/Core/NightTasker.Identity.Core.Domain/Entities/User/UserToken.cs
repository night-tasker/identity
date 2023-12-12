using Microsoft.AspNetCore.Identity;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Identity.Domain.Entities.User;

/// <summary>
/// Токен пользователя.
/// </summary>
public class UserToken : IdentityUserToken<Guid>, IEntity, ICreatedDateTimeOffset
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}