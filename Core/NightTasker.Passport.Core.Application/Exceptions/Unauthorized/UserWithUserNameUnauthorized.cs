using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

public class UserWithUserNameUnauthorized : UnauthorizedException
{
    public UserWithUserNameUnauthorized(string username) : base($"User with username: {username} not found")
    {
    }
}