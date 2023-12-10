using MediatR;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Application.Models.Jwt;

namespace NightTasker.Identity.Application.Features.Users.Commands.RefreshToken;

/// <summary>
/// Команда для обновления access-токена пользователя.
/// </summary>
/// <param name="RefreshToken">Рефреш-токен пользователя.</param>
public record RefreshTokenCommand(RefreshUserTokenDto RefreshToken) : IRequest<JwtAccessToken>;