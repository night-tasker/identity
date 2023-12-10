using NightTasker.Identity.Application.Exceptions.Base;

namespace NightTasker.Identity.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение невалидного рефреш-токена пользователя.
/// </summary>
public class InvalidUserRefreshTokenUnauthorizedException() : UnauthorizedException("User refresh token is invalid");