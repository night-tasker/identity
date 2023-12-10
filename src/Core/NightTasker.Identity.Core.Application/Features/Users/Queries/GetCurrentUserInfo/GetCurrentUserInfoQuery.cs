using MediatR;
using NightTasker.Identity.Application.Features.Users.Models;

namespace NightTasker.Identity.Application.Features.Users.Queries.GetCurrentUserInfo;

/// <summary>
/// Запрос для получения информация о текущем пользователе.
/// </summary>
public record GetCurrentUserInfoQuery : IRequest<CurrentUserInfo>;