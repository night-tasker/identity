﻿using Microsoft.AspNetCore.Identity;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Domain.Entities.User;

namespace NightTasker.Identity.Application.ApplicationContracts.Identity;

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
    /// <param name="userName">Имя пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат проверки.</returns>
    Task<bool> IsUserNameExist(string userName, CancellationToken cancellationToken);

    /// <summary>
    /// Проверить данные пользователя на вход.
    /// </summary>
    /// <param name="userDto">Данные пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<User> ValidateLoginUser(LoginUserDto userDto, CancellationToken cancellationToken);
}