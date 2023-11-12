using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неаутентификации пользователя с конкретным именем.
/// </summary>
public class UserWithUserNameUnauthorizedException : UnauthorizedException
{
    public UserWithUserNameUnauthorizedException(string username) : base($"User with username: {username} not found")
    {
    }
}