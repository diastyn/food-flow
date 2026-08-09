using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Errors;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Specifications;
using FoodFlow.Modules.Identity.Domain.Auth;
using FoodFlow.Modules.Identity.Domain.Auth.Contracts;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Auth.Commands;

public sealed class AuthenticateUserCommandHandler(
    IPasswordHasher passwordHasher,
    IJwtTokenIssuer tokenIssuer,
    TimeProvider timeProvider,
    [FromKeyedServices(nameof(Identity))] IUnitOfWork unitOfWork,
    IUserStore userStore) : IRequestHandler<AuthenticateUserCommand, Result<AuthToken>>
{
    public async Task<Result<AuthToken>> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var spec = new UserSpecification()
            .ByUsername(request.Username);

        var user = await userStore.GetAsync(spec, cancellationToken);
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash.ToString()))
        {
            return Result.Failure<AuthToken>(UsersErrors.Application.UserUnauthorized.New());
        }

        user.RecordLogin();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var sessionId = Guid.NewGuid();
        var now = timeProvider.GetUtcNow();
        var access = tokenIssuer.IssueAccessToken(user, sessionId);

        return Result.Success(new AuthToken(
            access.Token,
            (access.ExpiresAt - now).TotalSeconds));
    }
}
