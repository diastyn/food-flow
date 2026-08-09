using FoodFlow.API.Contracts;
using FoodFlow.BuildingBlocks.Domain.Security;
using FoodFlow.Modules.Identity.Application.Auth.Commands;
using FoodFlow.Modules.Identity.Application.Users.Queries;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    IRequestContext requestContext,
    ISender sender) : ControllerBase
{
    [HttpPost("token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetToken(
        [FromBody] GetTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new AuthenticateUserCommand(request.Username, request.Password),
            cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Возвращает данные текущего аутентифицированного пользователя по claim <c>sub</c>.
    /// </summary>
    /// <param name="cancellation">Токен отмены операции.</param>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserModel>> GetMe(
        CancellationToken cancellation)
    {
        var id = requestContext.UserId;
        if (id is null)
        {
            return Unauthorized();
        }

        var result = await sender.Send(new GetUserByIdQuery(id.Value), cancellation);
        return result.ToActionResult(HttpContext);
    }
}
