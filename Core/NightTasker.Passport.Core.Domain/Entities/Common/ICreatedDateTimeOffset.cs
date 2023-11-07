namespace NightTasker.Passport.Domain.Entities.Common;

public interface ICreatedDateTimeOffset
{
    /// <summary>
    /// Creation date and time.
    /// </summary>
    DateTimeOffset CreatedDateTimeOffset { get; set; }
}