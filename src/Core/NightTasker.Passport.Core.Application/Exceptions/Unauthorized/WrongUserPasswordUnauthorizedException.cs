using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неправильного пароля пользователя.
/// </summary>
public class WrongUserPasswordUnauthorizedException(string username) : UnauthorizedException(
    $"User with username: {username} entered wrong password");