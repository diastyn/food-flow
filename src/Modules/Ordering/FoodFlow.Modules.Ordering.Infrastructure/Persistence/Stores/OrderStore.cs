using AutoMapper;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.Stores;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Stores;

internal sealed class OrderStore(
    OrderingDbContext dbContext,
    IMapper mapper) : EfCoreStore<Order, OrderId>(dbContext, mapper), IOrderStore;
