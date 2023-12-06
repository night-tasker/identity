namespace NightTasker.Passport.Application.Features.Users.Models;

/// <summary>
/// DTO для входа пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
/// <param name="Password">Пароль.</param>
public record LoginUserDto(string UserName, string Password);