using MediatR;
using NightTasker.Passport.Application.Features.Users.Models;

namespace NightTasker.Passport.Application.Features.Users.Queries.GetCurrentUserInfo;

public record GetCurrentUserInfoQuery : IRequest<CurrentUserInfo>;