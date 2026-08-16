using AutoMapper;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Mappings;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        _ = CreateMap<Order, OrderModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId.Value))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.TotalPrice.Currency.Code))
            .ForMember(
                dest => dest.OrderItems,
                opt => opt.MapFrom(src => src.Items));

        _ = CreateMap<Address, AddressModel>();
    }
}
