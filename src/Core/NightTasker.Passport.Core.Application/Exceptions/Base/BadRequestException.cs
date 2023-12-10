using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// BadRequest (400) исключение.
/// </summary>
public abstract class BadRequestException(string message) : Exception(message), IStatusCodeException
{
    /// <inheritdoc />
    public int StatusCode { get; init; } = (int)HttpStatusCode.BadRequest;
}