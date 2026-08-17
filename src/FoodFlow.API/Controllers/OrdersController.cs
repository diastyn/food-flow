using FoodFlow.BuildingBlocks.API.ApiResults;
using FoodFlow.BuildingBlocks.API.Controllers;
using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Ordering.Application.Orders.Commands.CreateOrder;
using FoodFlow.Modules.Ordering.Application.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API.Controllers;

public sealed class OrdersController(ISender sender) : ApiBaseController
{
    [HttpPost]
    [Authorize(Policy = AppPermissions.Orders.Write)]
    public async Task<ActionResult> CreateOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(HttpContext);
    }

    [HttpGet]
    [Authorize(Policy = AppPermissions.Orders.Read)]
    public async Task<ActionResult> GetOrders(
        [FromQuery] GetOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(HttpContext);
    }
}
