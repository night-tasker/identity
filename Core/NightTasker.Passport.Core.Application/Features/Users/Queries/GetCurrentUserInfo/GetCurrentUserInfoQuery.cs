using MediatR;
using NightTasker.Passport.Application.Features.Users.Models;

namespace NightTasker.Passport.Application.Features.Users.Queries.GetCurrentUserInfo;

/// <summary>
/// Запрос для получения информация о текущем пользователе.
/// </summary>
public record GetCurrentUserInfoQuery : IRequest<CurrentUserInfo>;