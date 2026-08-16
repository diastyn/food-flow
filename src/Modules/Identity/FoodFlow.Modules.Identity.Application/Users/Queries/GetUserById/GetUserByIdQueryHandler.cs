using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler(
    IUserStore userStore) : IRequestHandler<GetUserByIdQuery, Result<UserModel>>
{
    public async Task<Result<UserModel>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userStore.GetByIdAsync<UserModel>(
            new UserId(request.Id),
            cancellationToken);

        return user is null
            ? Result.Failure<UserModel>(AppErrors.Application.UserNotFound.New())
            : Result.Success(user);
    }
}
