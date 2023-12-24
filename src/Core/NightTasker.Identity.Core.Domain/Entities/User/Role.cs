using Microsoft.AspNetCore.Identity;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Identity.Domain.Entities.User;

/// <summary>
/// Роль пользователя.
/// </summary>
public class Role : IdentityRole<Guid>, IEntityWithId<Guid>, IDateTimeOffsetModification
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
 
    /// <inheritdoc />
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
}