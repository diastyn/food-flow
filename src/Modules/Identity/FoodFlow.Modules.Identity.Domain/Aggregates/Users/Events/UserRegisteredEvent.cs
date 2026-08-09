using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Events;

public sealed record UserRegisteredEvent(
    Guid UserId,
    string Username,
    string Email) : DomainEvent;
