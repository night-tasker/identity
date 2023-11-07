namespace NightTasker.Passport.Domain.Entities.Common;

public interface IUpdatedDateTimeOffset
{
    /// <summary>
    /// Update date and time.
    /// </summary>
    DateTimeOffset UpdatedDateTimeOffset { get; set; }
}