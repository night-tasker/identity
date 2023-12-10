namespace NightTasker.Identity.Presentation.WebApi.Requests.User;

/// <summary>
/// Запрос на создание пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="Password">Пароль пользователя.</param>
/// <param name="ConfirmPassword">Подтверждение пароля.</param>
public record CreateUserRequest(string UserName, string Password, string ConfirmPassword);