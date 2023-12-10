namespace NightTasker.Identity.Application.Exceptions.Base;

/// <summary>
/// Exception с HTTP-статус кодом.
/// </summary>
public interface IStatusCodeException
{
    /// <summary>
    /// Статус-код исключения.
    /// </summary>
    int StatusCode { get; }
}