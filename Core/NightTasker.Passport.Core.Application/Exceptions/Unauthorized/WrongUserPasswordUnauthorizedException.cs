using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

public class WrongUserPasswordUnauthorizedException : UnauthorizedException
{
    public WrongUserPasswordUnauthorizedException(string username) : base($"User with username: {username} entered wrong password")
    {
    }
}