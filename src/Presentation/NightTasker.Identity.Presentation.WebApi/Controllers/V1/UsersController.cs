﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NightTasker.Identity.Application.Features.Users.Commands.Create;
using NightTasker.Identity.Application.Features.Users.Commands.Login;
using NightTasker.Identity.Application.Features.Users.Commands.RefreshToken;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Application.Features.Users.Queries.GetCurrentUserInfo;
using NightTasker.Identity.Presentation.WebApi.Constants;
using NightTasker.Identity.Presentation.WebApi.Endpoints;
using NightTasker.Identity.Presentation.WebApi.Requests.User;

namespace NightTasker.Identity.Presentation.WebApi.Controllers.V1;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route($"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}")]
public class UsersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <summary>
    /// Создание пользователя.
    /// </summary>
    /// <param name="request">Запрос на создание пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost(UsersEndpoints.UserRegister)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IdentityResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var createUserDto = _mapper.Map<CreateUserDto>(request);
        var command = new CreateUserCommand(createUserDto);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Succeeded ? Ok() : BadRequest(result);
    }
    
    /// <summary>
    /// Вход пользователя в систему.
    /// </summary>
    /// <param name="request">Запрос на вход в систему.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Токен доступа.</returns>
    [HttpPost(UsersEndpoints.UserLogin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginUser(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var loginUserDto = _mapper.Map<LoginUserDto>(request);
        var command = new LoginUserCommand(loginUserDto);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получение информации о текущем пользователе.
    /// </summary>
    /// <returns>Информация о текущем пользователе.</returns>
    [Authorize]
    [HttpGet(UsersEndpoints.CurrentUserInfo)]
    [ProducesResponseType(typeof(CurrentUserInfo), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var query = new GetCurrentUserInfoQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Обновление токена авторизации.
    /// </summary>
    /// <param name="request">Запрос на обновление токена авторизации</param>
    /// <returns></returns>
    [HttpPost(UsersEndpoints.UserRefreshToken)]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        var refreshUserTokenDto = _mapper.Map<RefreshUserTokenDto>(request);
        var command = new RefreshTokenCommand(refreshUserTokenDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}