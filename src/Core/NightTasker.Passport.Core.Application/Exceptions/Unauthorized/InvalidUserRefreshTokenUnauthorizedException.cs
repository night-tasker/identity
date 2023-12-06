using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение невалидного рефреш-токена пользователя.
/// </summary>
public class InvalidUserRefreshTokenUnauthorizedException : UnauthorizedException
{
    public InvalidUserRefreshTokenUnauthorizedException() : base("User refresh token is invalid")
    {
    }
}