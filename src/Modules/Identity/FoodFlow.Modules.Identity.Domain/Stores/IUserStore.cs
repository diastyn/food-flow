using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Stores;

public interface IUserStore : IStore<User, UserId>
{
    public Task AddAsync(User user, CancellationToken cancellationToken);
}
