using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// Unauthorized (401) исключение.
/// </summary>
public abstract class UnauthorizedException : Exception, IStatusCodeException
{
    protected UnauthorizedException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.Unauthorized;
    }

    /// <inheritdoc />
    public int StatusCode { get; init; }
}