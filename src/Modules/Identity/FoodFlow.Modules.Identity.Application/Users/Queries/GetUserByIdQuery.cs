using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Users.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserModel>>;
