namespace NightTasker.Passport.Application.Exceptions.Base;

public interface IStatusCodeException
{
    int StatusCode { get; init; }
}