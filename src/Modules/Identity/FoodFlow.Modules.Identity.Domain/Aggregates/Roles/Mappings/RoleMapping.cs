using AutoMapper;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Mappings;

public sealed class RoleMapping : Profile
{
    public RoleMapping()
    {
        _ = CreateMap<Role, RoleModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
    }
}
