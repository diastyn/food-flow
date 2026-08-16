using FoodFlow.API.ApiResults;
using FoodFlow.API.Contracts;
using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Application.Users.Commands.AssignRole;
using FoodFlow.Modules.Identity.Application.Users.Commands.RegisterUser;
using FoodFlow.Modules.Identity.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API.Controllers;

[Route("api/users")]
public sealed class UserController(ISender sender) : ApiBaseController
{
    [HttpPost]
    [Authorize(Policy = AppPermissions.Users.Write)]
    public async Task<ActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = AppPermissions.Users.Read)]
    public async Task<ActionResult> GetUserById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserByIdQuery(id), cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    [HttpPost("{id:guid}/roles")]
    [Authorize(Policy = AppPermissions.Users.ManageRoles)]
    public async Task<ActionResult> AssignRoleToUser(
        [FromRoute] Guid id,
        [FromBody] AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new AssignRoleCommand(id, request.RoleName),
            cancellationToken);
        return result.ToActionResult(HttpContext);
    }
}
