using FoodFlow.BuildingBlocks.Application.Queries;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;

namespace FoodFlow.Modules.Identity.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IQueryRequest<Result<UserModel>>;
