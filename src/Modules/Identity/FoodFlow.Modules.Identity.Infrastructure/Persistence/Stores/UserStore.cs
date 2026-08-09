using AutoMapper;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Stores;

internal class UserStore(
    IdentityDbContext dbContext,
    IMapper mapper)
    : EfCoreStore<User, UserId>(dbContext, mapper), IUserStore
{
    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        _ = await _dbSet.AddAsync(user, cancellationToken);
    }
}
