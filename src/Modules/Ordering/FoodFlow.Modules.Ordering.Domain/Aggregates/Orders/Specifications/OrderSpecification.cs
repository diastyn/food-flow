using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Specifications;

public class OrderSpecification : AggregateSpecification<Order, OrderId>
{
    public OrderSpecification ByRestaurantId(Guid restaurantId)
    {
        _ = Query.Where(o => o.RestaurantId == restaurantId);

        return this;
    }

    public OrderSpecification IncludeOrderItems()
    {
        _ = Query.Include(o => o.Items);

        return this;
    }
}
