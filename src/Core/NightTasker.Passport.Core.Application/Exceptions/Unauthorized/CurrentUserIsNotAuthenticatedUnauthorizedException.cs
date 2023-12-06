using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неаутентификации текущего пользователя.
/// </summary>
public class CurrentUserIsNotAuthenticatedUnauthorizedException : UnauthorizedException
{
    public CurrentUserIsNotAuthenticatedUnauthorizedException() : base("User is not authenticated")
    {
    }
}