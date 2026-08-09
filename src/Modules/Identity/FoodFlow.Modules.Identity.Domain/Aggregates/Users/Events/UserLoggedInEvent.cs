using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Events;

/// <summary>Поднимается при успешном входе в систему.</summary>
public sealed record UserLoggedInEvent(Guid UserId, DateTimeOffset At) : DomainEvent;
