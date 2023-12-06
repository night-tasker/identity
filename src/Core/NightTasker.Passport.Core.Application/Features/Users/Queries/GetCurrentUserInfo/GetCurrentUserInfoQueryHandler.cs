using MapsterMapper;
using MediatR;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Application.Exceptions.Unauthorized;
using NightTasker.Passport.Application.Features.Users.Models;

namespace NightTasker.Passport.Application.Features.Users.Queries.GetCurrentUserInfo;

/// <summary>
/// Хэндлер запроса для получения информации о текущем пользователе.
/// </summary>
public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, CurrentUserInfo>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCurrentUserInfoQueryHandler(
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
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