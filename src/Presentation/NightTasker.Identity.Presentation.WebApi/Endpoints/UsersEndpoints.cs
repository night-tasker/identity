namespace NightTasker.Identity.Presentation.WebApi.Endpoints;

/// <summary>
/// Пути эндпоинтов для работы с пользователями.
/// </summary>
public static class UsersEndpoints
{
    /// <summary>
    /// Путь до эндпоинтов для работы с пользователями (основной).
    /// </summary>
    public const string UsersResource = "users";
    
    /// <summary>
    /// Путь для регистрации пользователя.
    /// </summary>
    public const string UserRegister = "register";

    /// <summary>
    /// Путь для входа пользователя.
    /// </summary>
    public const string UserLogin = "login";

    /// <summary>
    /// Путь для получения информации о текущем пользователе.
    /// </summary>
    public const string CurrentUserInfo = "current";

    /// <summary>
    /// Путь для обновления токена доступа пользователя.
    /// </summary>
    public const string UserRefreshToken = "refresh-token";
}