using System.Net;
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
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorDetails = new ErrorDetails
        {
            TraceId = context.TraceIdentifier
        };

        context.Response.StatusCode = ResolveStatusCode(exception);
        errorDetails.Message = ResolveMessage(exception);
        
        return context.Response.WriteAsJsonAsync(errorDetails);
    }

    private static int ResolveStatusCode(Exception exception)
    {
        return exception is IStatusCodeException statusCodeException
            ? statusCodeException.StatusCode
            : StatusCodes.Status500InternalServerError;
    }

    private static string ResolveMessage(Exception exception)
    {
        return exception is IPublicException publicException
            ? publicException.DisplayMessage
            : ErrorDetails.DefaultErrorMessage;
    }
}