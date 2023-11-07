using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.BadRequest;

/// <summary>
/// Публичное исключение при совпадении имени пользователя.
/// </summary>
public class UserNameExistsBadRequestException : BadRequestException, IPublicException
{
    public UserNameExistsBadRequestException(string username) : base($"User with {username} exists yet")
    {
        DisplayMessage = $"Пользователь с таким именем уже существует";
    }

    public string DisplayMessage { get; init; }
}