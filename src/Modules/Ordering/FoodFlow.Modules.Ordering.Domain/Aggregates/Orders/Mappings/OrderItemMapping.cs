using AutoMapper;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Mappings;

public class OrderItemMapping : Profile
{
    public OrderItemMapping()
    {
        _ = CreateMap<OrderItem, OrderItemModel>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value));
    }
}
