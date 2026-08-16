using FoodFlow.API.ApiResults;
using FoodFlow.API.Contracts;
using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Application.Roles.Commands.CreateRole;
using FoodFlow.Modules.Identity.Application.Roles.Commands.GrantPermission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API.Controllers;

public sealed class RolesController(ISender sender) : ApiBaseController
{
    [HttpPost]
    [Authorize(Policy = AppPermissions.Roles.Write)]
    public async Task<ActionResult> CreateRole(
        [FromBody] CreateRoleCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    [HttpPost("{id:guid}/permissions")]
    [Authorize(Policy = AppPermissions.Roles.ManagePermissions)]
    public async Task<ActionResult> GrantPermissions(
        [FromRoute] Guid id,
        [FromBody] GrantPermissionsRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new GrantPermissionsCommand(id, request.Permissions);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(HttpContext);
    }
}
