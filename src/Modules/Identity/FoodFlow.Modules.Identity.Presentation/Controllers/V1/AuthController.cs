using FoodFlow.BuildingBlocks.API.ApiResults;
using FoodFlow.BuildingBlocks.API.Controllers;
using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.BuildingBlocks.Domain.Authentication;
using FoodFlow.Modules.Identity.Application.Auth.Commands;
using FoodFlow.Modules.Identity.Application.Users.Queries.GetUserById;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using FoodFlow.Modules.Identity.Presentation.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Modules.Identity.Presentation.Controllers.V1;

public sealed class AuthController(
    IRequestContext requestContext,
    ISender sender) : ApiBaseController
{
    [HttpPost("token")]
    [AllowAnonymous]
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
    [Authorize(Policy = AppPermissions.Users.Read)]
    public async Task<ActionResult<UserModel>> GetMe(
        CancellationToken cancellation)
    {
        var id = requestContext.UserId;
        if (id is null)
        {
            return Unauthorized("User not found.");
        }

        var result = await sender.Send(new GetUserByIdQuery(id.Value), cancellation);
        return result.ToActionResult(HttpContext);
    }
}
