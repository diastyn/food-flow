using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Users.Commands;

public sealed record RegisterUserCommand(
    PersonNameModel Name,
    string Username,
    string Email,
    string Password,
    string? Phone) : IRequest<Result<Guid>>;
