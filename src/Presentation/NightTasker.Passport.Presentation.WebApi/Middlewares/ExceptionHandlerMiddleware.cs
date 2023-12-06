using NightTasker.Passport.Application.Exceptions.Base;
using NightTasker.Passport.Application.Models.Error;

namespace NightTasker.Passport.Presentation.Middlewares;

/// <summary>
/// Middleware для глобальной обработки исключений.
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
            _logger.LogError(exception.ToString());
        }
    }

    /// <summary>
    /// Обработать исключение
    /// </summary>
    /// <param name="context">HTTP-контекст.</param>
    /// <param name="exception">Исключение.</param>
    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorDetails = new ErrorDetails
        {
            TraceId = context.TraceIdentifier
        };

        context.Response.StatusCode = ResolveStatusCode(exception);
        errorDetails.Message = ErrorDetails.DefaultErrorMessage;
        errorDetails.DisplayMessage = ResolveDisplayMessage(exception);
        
        return context.Response.WriteAsJsonAsync(errorDetails);
    }

    /// <summary>
    /// Разрешить статус-код, исходя из исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Код.</returns>
    private static int ResolveStatusCode(Exception exception)
    {
        return exception is IStatusCodeException statusCodeException
            ? statusCodeException.StatusCode
            : StatusCodes.Status500InternalServerError;
    }

    /// <summary>
    /// Разрешить сообщение для отображения, исходя из исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Сообщение для отображения.</returns>
    private static string? ResolveDisplayMessage(Exception exception)
    {
        return exception is IPublicException publicException
            ? publicException.DisplayMessage
            : null;
    }
}