using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Specifications;

public sealed class UserSpecification : AggregateSpecification<User, UserId>
{
    public UserSpecification ByEmail(string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);

        Query.Where(u => u.Email == email);

        return this;
    }

    public UserSpecification ByUsername(string username)
    {
        ArgumentException.ThrowIfNullOrEmpty(username);

        Query.Where(u => u.Username == username);

        return this;
    }
}
