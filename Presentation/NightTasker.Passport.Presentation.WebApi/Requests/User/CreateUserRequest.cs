namespace NightTasker.Passport.Presentation.Requests.User;

/// <summary>
/// Запрос на создание пользователя.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password { get; set; } = null!;


    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    public string ConfirmPassword { get; set; } = null!;
}