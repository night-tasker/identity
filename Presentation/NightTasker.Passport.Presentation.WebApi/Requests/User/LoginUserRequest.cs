namespace NightTasker.Passport.Presentation.Requests.User;

/// <summary>
/// Запрос на вход пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="Password">Пароль.</param>
public record LoginUserRequest(string UserName, string Password);