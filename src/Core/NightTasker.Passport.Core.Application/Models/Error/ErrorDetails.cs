namespace NightTasker.Passport.Application.Models.Error;

/// <summary>
/// Детали ошибки (исключения).
/// </summary>
public class ErrorDetails
{
    public string Message { get; set; } = null!;

    public string? DisplayMessage { get; set; }

    public string TraceId { get; set; } = null!;

    public const string DefaultErrorMessage = "An error occurred while processing your request.";
}