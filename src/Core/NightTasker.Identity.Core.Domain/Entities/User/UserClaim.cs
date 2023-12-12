using Microsoft.AspNetCore.Identity;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Identity.Domain.Entities.User;

/// <summary>
/// Клейм пользователя.
/// </summary>
public class UserClaim : IdentityUserClaim<Guid>, IEntityWithId<int>, IDateTimeOffsetModification
{
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset UpdatedDateTimeOffset { get; set; }
}