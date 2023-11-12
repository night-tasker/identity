using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Identity;

/// <summary>
/// Сервис пользователей.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Создать (зарегистрировать) пользователя.
    /// </summary>
    /// <param name="userDto">Данные пользователя.</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат создания.</returns>
    Task<IdentityResult> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken);

    /// <summary>
    /// Проверить существует ли пользователь с данным именем.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат проверки.</returns>
    Task<bool> IsUserNameExist(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Проверить данные пользователя на вход.
    /// </summary>
    /// <param name="userDto">Данные пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<User> ValidateLoginUser(LoginUserDto userDto, CancellationToken cancellationToken);
}