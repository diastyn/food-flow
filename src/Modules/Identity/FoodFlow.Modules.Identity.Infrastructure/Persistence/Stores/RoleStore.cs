using AutoMapper;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Stores;

internal sealed class RoleStore(
    IdentityDbContext dbContext,
    IMapper mapper)
    : EfCoreStore<Role, RoleId>(dbContext, mapper),
        IRoleStore;
