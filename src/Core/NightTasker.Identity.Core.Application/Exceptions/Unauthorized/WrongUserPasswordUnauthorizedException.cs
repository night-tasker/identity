using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.Identity.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неправильного пароля пользователя.
/// </summary>
public class WrongUserPasswordUnauthorizedException(string username) : UnauthorizedException(
    $"User with username: {username} entered wrong password");