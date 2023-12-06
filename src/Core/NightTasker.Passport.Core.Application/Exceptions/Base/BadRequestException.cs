using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// BadRequest (400) исключение.
/// </summary>
public abstract class BadRequestException : Exception, IStatusCodeException
{
    protected BadRequestException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
    }
    
    /// <inheritdoc />
    public int StatusCode { get; init; }
}