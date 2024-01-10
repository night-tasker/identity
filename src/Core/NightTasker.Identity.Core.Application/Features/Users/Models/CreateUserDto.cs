namespace NightTasker.Identity.Application.Features.Users.Models;

/// <summary> 
/// DTO для создания (регистрации) пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="Email">Электронная почта.</param>
/// <param name="Password">Пароль.</param>
public record CreateUserDto(string UserName, string Email, string Password);