using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// NotFound (404) исключение.
/// </summary>
public abstract class NotFoundException(string message) : Exception(message), IStatusCodeException
{
    /// <inheritdoc />
    public int StatusCode { get; } = (int)HttpStatusCode.NotFound;
}