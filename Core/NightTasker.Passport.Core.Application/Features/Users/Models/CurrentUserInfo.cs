namespace NightTasker.Passport.Application.Features.Users.Models;

/// <summary>
/// DTO информации текущего пользователя.
/// </summary>
/// <param name="Id">ИД пользователя.</param>
/// <param name="UserName">Имя пользователя.</param>
public record CurrentUserInfo(Guid Id, string UserName);