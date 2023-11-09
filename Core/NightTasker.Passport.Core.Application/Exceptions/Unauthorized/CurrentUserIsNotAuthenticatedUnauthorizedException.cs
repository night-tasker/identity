using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

public class CurrentUserIsNotAuthenticatedUnauthorizedException : UnauthorizedException
{
    public CurrentUserIsNotAuthenticatedUnauthorizedException() : base("User is not authenticated")
    {
    }
}