using MediatR;
using Microsoft.AspNetCore.Identity;
using NightTasker.Identity.Application.Features.Users.Models;

namespace NightTasker.Identity.Application.Features.Users.Commands.Create;

/// <summary>
/// Команда для создания (регистрации) пользователя.
/// </summary>
/// <param name="User"></param>
public record CreateUserCommand(CreateUserDto User) : IRequest<IdentityResult>;