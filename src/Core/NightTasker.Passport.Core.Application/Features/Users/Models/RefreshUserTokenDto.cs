namespace NightTasker.Passport.Application.Features.Users.Models;



/// <summary>
/// DTO для обновления access-токена пользователя.
/// </summary>
/// <param name="RefreshTokenId">ИД рефреш-токена.</param>
public record RefreshUserTokenDto(Guid RefreshTokenId);