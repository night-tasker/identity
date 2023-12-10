using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение невалидного рефреш-токена пользователя.
/// </summary>
public class InvalidUserRefreshTokenUnauthorizedException() : UnauthorizedException("User refresh token is invalid");