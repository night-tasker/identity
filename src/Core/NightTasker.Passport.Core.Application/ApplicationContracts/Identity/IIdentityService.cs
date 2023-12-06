namespace NightTasker.Passport.Application.ApplicationContracts.Identity;

/// <summary>
/// Сервис для работы с аутентификацией.
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// ИД текущего пользователя.
    /// </summary>
    Guid? CurrentUserId { get; }
    
    /// <summary>
    ///  Аутентифицирован ли текущий пользователь.
    /// </summary>
    bool IsAuthenticated { get; }
}