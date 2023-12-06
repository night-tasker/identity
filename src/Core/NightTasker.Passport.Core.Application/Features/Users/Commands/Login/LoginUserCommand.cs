using MediatR;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Application.Models.Jwt;

namespace NightTasker.Passport.Application.Features.Users.Commands.Login;

/// <summary>
/// Команда на вход пользователя.
/// </summary>
/// <param name="UserDto">Данные пользователя для входа.</param>
public record LoginUserCommand(LoginUserDto UserDto) : IRequest<JwtAccessToken>;