﻿using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NightTasker.Common.Messaging.Events.Contracts;
using NightTasker.Common.Messaging.Events.Implementations;
using NightTasker.Identity.Application.ApplicationContracts.Identity;
using NightTasker.Identity.Application.Exceptions.BadRequest;
using NightTasker.Identity.Application.Exceptions.Unauthorized;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Identity.Identity.Contracts;

namespace NightTasker.Identity.Infrastructure.Identity.Implementations.Services;

/// <inheritdoc />
public class UserService(IAppUserManager userManager,
        IMapper mapper,
        ILogger<UserService> logger,
        IPublishEndpoint publishEndpoint)
    : IUserService
{
    private readonly IAppUserManager _appUserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IPublishEndpoint _publishEndpoint =
        publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));

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

        var userRegistered = new UserRegistered(user.Id, user.UserName!);
        await _publishEndpoint.Publish<IUserRegistered>(userRegistered, cancellationToken);
        
        return IdentityResult.Success;
    }

    /// <inheritdoc />
    public async Task<bool> IsUserNameExist(string userName, CancellationToken cancellationToken)
    {
        var user = await _appUserManager.FindByNameAsync(userName);
        return user is not null;
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