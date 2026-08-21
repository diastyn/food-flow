using FoodFlow.BuildingBlocks.Application.Commands;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    PersonNameModel Name,
    string Username,
    string Email,
    string Password,
    string? Phone) : ICommandRequest<Result<Guid>>;
