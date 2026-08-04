using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using FoodFlow.Modules.Ordering.Domain.Stores;
using MediatR;

namespace FoodFlow.Modules.Ordering.Application.Orders.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler(
    IOrderStore store) 
    : IRequestHandler<GetOrdersQuery, Result<IReadOnlyList<OrderModel>>>
{
    public async Task<Result<IReadOnlyList<OrderModel>>> Handle(
        GetOrdersQuery request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var orders = await store.GetManyAsync(cancellationToken);
        
        return Result.Success(orders);
    }
}