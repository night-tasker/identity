namespace NightTasker.Passport.Presentation.Requests.User;

/// <summary>
/// Запрос на обновление токена пользователя.
/// </summary>
/// <param name="RefreshToken">Рефреш-токен пользователя.</param>
public record RefreshTokenRequest(Guid RefreshToken);