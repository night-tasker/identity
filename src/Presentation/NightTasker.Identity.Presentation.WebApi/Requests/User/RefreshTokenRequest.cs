namespace NightTasker.Identity.Presentation.WebApi.Requests.User;

/// <summary>
/// Запрос на обновление токена пользователя.
/// </summary>
/// <param name="RefreshToken">Рефреш-токен пользователя.</param>
public record RefreshTokenRequest(Guid RefreshToken);