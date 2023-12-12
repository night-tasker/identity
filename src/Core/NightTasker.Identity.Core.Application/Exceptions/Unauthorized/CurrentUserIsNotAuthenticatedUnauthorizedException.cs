using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.Identity.Application.Exceptions.Unauthorized;

/// <summary>
/// Исключение неаутентификации текущего пользователя.
/// </summary>
public class CurrentUserIsNotAuthenticatedUnauthorizedException() : UnauthorizedException("User is not authenticated");