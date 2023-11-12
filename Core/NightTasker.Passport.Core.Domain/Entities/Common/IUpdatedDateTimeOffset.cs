namespace NightTasker.Passport.Domain.Entities.Common;

/// <summary>
/// Дата и время обновления.
/// </summary>
public interface IUpdatedDateTimeOffset
{
    /// <summary>
    /// Дата и время обновления.
    /// </summary>
    DateTimeOffset UpdatedDateTimeOffset { get; set; }
}