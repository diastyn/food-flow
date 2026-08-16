using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Stores;

public interface IOrderStore : IStore<Order, OrderId>;
