using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.Identity.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неаутентификации пользователя с конкретным именем.
/// </summary>
public class UserWithUserNameUnauthorizedException(string username) : UnauthorizedException($"User with username: {username} not found");