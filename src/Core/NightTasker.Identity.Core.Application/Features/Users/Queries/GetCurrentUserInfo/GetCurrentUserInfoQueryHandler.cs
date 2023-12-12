using MapsterMapper;
using MediatR;
using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.Identity.Application.ApplicationContracts.Identity;
using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Application.Exceptions.Unauthorized;
using NightTasker.Identity.Application.Features.Users.Models;

namespace NightTasker.Identity.Application.Features.Users.Queries.GetCurrentUserInfo;

/// <summary>
/// Хэндлер запроса для получения информации о текущем пользователе.
/// </summary>
public class GetCurrentUserInfoQueryHandler(
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    : IRequestHandler<GetCurrentUserInfoQuery, CurrentUserInfo>
{
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<CurrentUserInfo> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (!_identityService.IsAuthenticated || !_identityService.CurrentUserId.HasValue)
        {
            throw new CurrentUserIsNotAuthenticatedUnauthorizedException();
        }
        
        var user = await _unitOfWork.UserRepository.TryGetById(_identityService.CurrentUserId.Value, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("Current user not found");
        }
        
        var currentUserInfo = _mapper.Map<CurrentUserInfo>(user);
        return currentUserInfo;
    }
}