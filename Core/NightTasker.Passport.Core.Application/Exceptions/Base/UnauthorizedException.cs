using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

public abstract class UnauthorizedException : Exception, IStatusCodeException
{
    protected UnauthorizedException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.Unauthorized;
    }

    public int StatusCode { get; init; }
}