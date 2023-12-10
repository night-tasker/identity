using MediatR;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Application.Models.Jwt;

namespace NightTasker.Identity.Application.Features.Users.Commands.Login;

/// <summary>
/// Команда на вход пользователя.
/// </summary>
/// <param name="UserDto">Данные пользователя для входа.</param>
public record LoginUserCommand(LoginUserDto UserDto) : IRequest<JwtAccessToken>;