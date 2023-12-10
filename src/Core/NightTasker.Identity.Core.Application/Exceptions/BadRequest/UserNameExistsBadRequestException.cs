using NightTasker.Identity.Application.Exceptions.Base;

namespace NightTasker.Identity.Application.Exceptions.BadRequest;

/// <summary>
/// Публичное исключение при совпадении имени пользователя.
/// </summary>
public class UserNameExistsBadRequestException(string username) : BadRequestException(
    $"User with {username} exists yet"), IPublicException
{
    public string DisplayMessage { get; init; } = $"Пользователь с таким именем уже существует";
}