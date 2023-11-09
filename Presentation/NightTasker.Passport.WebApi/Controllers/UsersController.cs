using System.Net;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.Features.Users.Commands.Create;
using NightTasker.Passport.Application.Features.Users.Commands.LoginUser;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Application.Features.Users.Queries.GetCurrentUserInfo;
using NightTasker.Passport.Presentation.Constants;
using NightTasker.Passport.Presentation.Endpoints;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route($"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(
        IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

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
        var dto = request.Adapt<CreateUserDto>();
        var command = new CreateUserCommand(dto);
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
        var dto = request.Adapt<LoginUserDto>();
        var command = new LoginUserCommand(dto);
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
}