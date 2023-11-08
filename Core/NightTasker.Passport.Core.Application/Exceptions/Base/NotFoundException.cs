using System.Net;

namespace NightTasker.Passport.Application.Exceptions.Base;

public abstract class NotFoundException : Exception, IStatusCodeException
{
    protected NotFoundException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
    }

    public int StatusCode { get; init; }
}