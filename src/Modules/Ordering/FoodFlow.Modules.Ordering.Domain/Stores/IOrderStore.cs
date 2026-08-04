using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Stores;

public interface IOrderStore
{
    Task<Order?> GetAsync(OrderId orderId, CancellationToken cancellationToken);

    Task<IReadOnlyList<OrderModel>> GetManyAsync(CancellationToken cancellationToken);

    Task<OrderId> AddAsync(Order order, CancellationToken cancellationToken);
}