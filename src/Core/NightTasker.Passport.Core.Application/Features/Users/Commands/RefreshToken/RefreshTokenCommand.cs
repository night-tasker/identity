using MediatR;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Application.Models.Jwt;

namespace NightTasker.Passport.Application.Features.Users.Commands.RefreshToken;

/// <summary>
/// Команда для обновления access-токена пользователя.
/// </summary>
/// <param name="RefreshToken">Рефреш-токен пользователя.</param>
public record RefreshTokenCommand(RefreshUserTokenDto RefreshToken) : IRequest<JwtAccessToken>;