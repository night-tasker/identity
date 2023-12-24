using Microsoft.AspNetCore.Identity;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Identity.Domain.Entities.User;

/// <summary>
/// Информация о входе пользователя.
/// </summary>
public class UserLogin : IdentityUserLogin<Guid>, IDateTimeOffsetModification
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
}