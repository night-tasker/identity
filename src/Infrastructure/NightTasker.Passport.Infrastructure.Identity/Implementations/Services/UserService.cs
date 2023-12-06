using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.Exceptions.BadRequest;
using NightTasker.Passport.Application.Exceptions.Unauthorized;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Identity.Identity.Contracts;

namespace NightTasker.Passport.Infrastructure.Identity.Implementations.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IAppUserManager _appUserManager;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IAppUserManager userManager,
        IMapper mapper,
        ILogger<UserService> logger)
    {
        _appUserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <inheritdoc />
    public async Task<IdentityResult> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        if (await IsUserNameExist(userDto.UserName, cancellationToken))
        {
            _logger.LogInformation("User with username {UserName} exists", userDto.UserName);
            throw new UserNameExistsBadRequestException(userDto.UserName);
        }
        var user = _mapper.Map<User>(userDto);
        _logger.LogInformation("[STARTED] Create user with username {UserName}", userDto.UserName);
        
        var createIdentityResult = await _appUserManager.CreateAsync(user);
        var addPasswordIdentityResult = await _appUserManager.AddPasswordAsync(user, userDto.Password);
        
        if (!(createIdentityResult.Succeeded && addPasswordIdentityResult.Succeeded))
        {
            _logger.LogInformation("[FAILED] Create user with username {UserName}", userDto.UserName);

            var result = IdentityResult.Failed(
                createIdentityResult.Errors.Concat(addPasswordIdentityResult.Errors).ToArray());
            return result;
        }
        
        _logger.LogInformation("[COMPLETED] Create user with username {UserName}", userDto.UserName);
        return IdentityResult.Success;
    }

    /// <inheritdoc />
    public Task<bool> IsUserNameExist(string? username, CancellationToken cancellationToken)
    {
        return _appUserManager.Users.AnyAsync(x => x.UserName != null && x.UserName.Equals(username), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User> ValidateLoginUser(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _appUserManager.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("User with username {UserName} not found", userDto.UserName);
            throw new UserWithUserNameUnauthorizedException(userDto.UserName);
        }

        if (!await _appUserManager.CheckPasswordAsync(user, userDto.Password))
        {
            _logger.LogInformation("Wrong password for user with username {UserName}", userDto.UserName);
            throw new WrongUserPasswordUnauthorizedException(userDto.UserName);
        }

        return user;
    }
}