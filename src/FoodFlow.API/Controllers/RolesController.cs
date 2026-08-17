using FoodFlow.API.Contracts;
using FoodFlow.BuildingBlocks.API.ApiResults;
using FoodFlow.BuildingBlocks.API.Controllers;
using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Application.Roles.Commands.CreateRole;
using FoodFlow.Modules.Identity.Application.Roles.Commands.GrantPermission;
using FoodFlow.Modules.Identity.Application.Roles.Queries.GetRole;
using FoodFlow.Modules.Identity.Application.Roles.Queries.GetRoles;
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

    [HttpGet]
    [Authorize(Policy = AppPermissions.Roles.Read)]
    public async Task<ActionResult> GetRoles(
        [FromQuery] GetRolesQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = AppPermissions.Roles.Read)]
    public async Task<ActionResult> GetRole(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRoleQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(HttpContext);
    }
}
