using AutoMapper;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions.Contracts;

namespace FoodFlow.Modules.Identity.Domain.Entities.Permissions.Mappings;

public sealed class PermissionMapping : Profile
{
    public PermissionMapping()
    {
        _ = CreateMap<Permission, PermissionModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
    }
}
