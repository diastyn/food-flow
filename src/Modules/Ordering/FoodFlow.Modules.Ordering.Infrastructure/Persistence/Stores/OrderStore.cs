using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using FoodFlow.Modules.Ordering.Domain.Stores;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Stores;

internal sealed class OrderStore(OrderingDbContext dbContext) : IOrderStore
{
    public async Task<Order?> GetAsync(
        OrderId orderId,
        CancellationToken cancellationToken)
    {
        var order = await dbContext
            .Orders
            .Where(order => order.Id == orderId)
            .FirstOrDefaultAsync(cancellationToken);

        return order;
    }

    public async Task<IReadOnlyList<OrderModel>> GetManyAsync(CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Select(order => new OrderModel(
                order.Id,
                order.RestaurantId,
                order.CustomerId,
                order.CreatedAt,
                order.Status,
                order.TotalPrice.Amount,
                order.TotalPrice.Currency.Code,
                new AddressModel(
                    order.DeliveryAddress.Street,
                    order.DeliveryAddress.City,
                    order.DeliveryAddress.Country,
                    order.DeliveryAddress.PostalCode),
                order.Items.Select(item => new OrderItemModel(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity))))
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<OrderId> AddAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);
        _ = await dbContext.AddAsync(order, cancellationToken);
        return order.Id;
    }
}
