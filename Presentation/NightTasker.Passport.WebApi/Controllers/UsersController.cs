using System.Net;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NightTasker.Passport.Application.Features.Users.Commands.Create;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Presentation.Endpoints;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route(UsersEndpoints.UsersResource)]
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
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IdentityResult), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var dto = request.Adapt<CreateUserDto>();
        var command = new CreateUserCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Succeeded ? Ok() : BadRequest(result);
    }
}