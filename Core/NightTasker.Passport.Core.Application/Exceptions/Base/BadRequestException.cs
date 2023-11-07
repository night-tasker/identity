using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

public abstract class BadRequestException : Exception, IStatusCodeException
{
    protected BadRequestException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
    }

    public int StatusCode { get; init; }
}