using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// Unauthorized (401) исключение.
/// </summary>
public abstract class UnauthorizedException(string message) : Exception(message), IStatusCodeException
{
    /// <inheritdoc />
    public int StatusCode { get; } = (int)HttpStatusCode.Unauthorized;
}