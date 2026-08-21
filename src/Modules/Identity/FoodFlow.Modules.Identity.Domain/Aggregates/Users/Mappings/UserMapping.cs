using AutoMapper;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Mappings;

public sealed class UserMapping : Profile
{
    public UserMapping()
    {
        _ = CreateMap<User, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone == null ? null : src.Phone.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new PersonNameModel
            {
                Firstname = src.Name.FirstName,
                Lastname = src.Name.LastName,
                Fullname = src.Name.FullName,
            }));
    }
}
