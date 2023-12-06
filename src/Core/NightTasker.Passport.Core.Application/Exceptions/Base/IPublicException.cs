namespace NightTasker.Passport.Application.Exceptions.Base;

/// <summary>
/// Интерфейс, предназначенный для разделения исключений на публичные и непубличные.
/// </summary>
public interface IPublicException
{
    /// <summary>
    /// Сообщение для отображения.
    /// </summary>
    string DisplayMessage { get; init; }
}