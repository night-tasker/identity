using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// NotFound (404) исключение.
/// </summary>
public abstract class NotFoundException : Exception, IStatusCodeException
{
    protected NotFoundException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
    }
    
    /// <inheritdoc />
    public int StatusCode { get; init; }
}