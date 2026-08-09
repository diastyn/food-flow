using AutoMapper;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Mappings;

public sealed class UserMapping : Profile
{
    public UserMapping()
    {
        _ = CreateMap<User, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

        _ = CreateMap<PersonName, PersonNameModel>();
    }
}
