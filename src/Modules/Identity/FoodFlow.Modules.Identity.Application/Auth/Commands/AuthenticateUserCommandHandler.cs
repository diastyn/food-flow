using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Application.Audits.Notifications.AuditEventOccurred;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Specifications;
using FoodFlow.Modules.Identity.Domain.Auth;
using FoodFlow.Modules.Identity.Domain.Auth.Contracts;
using FoodFlow.Modules.Identity.Domain.Entities.Audits.Enums;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Auth.Commands;

public sealed class AuthenticateUserCommandHandler(
    IPublisher publisher,
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
            .ByUsername(request.Username)
            .IncludeRolesAndPermissions();

        var user = await userStore.GetAsync(spec, cancellationToken);
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash.ToString()))
        {
            var error = AppErrors.Application.UserUnauthorized.New();
            await publisher.Publish(new AuditEventOccurredNotification(
                AuditAction.LoginFailed,
                ActorUserId: null,
                TargetUserId: user?.Id,
                request.Username,
                Details: error.Messages,
                Succeeded: false), cancellationToken);
            return Result.Failure<AuthToken>(error);
        }

        try
        {
            user.RecordLogin();
        }
        catch (DomainException)
        {
            await publisher.Publish(new AuditEventOccurredNotification(
                AuditAction.LoginBlockedDeactivated,
                ActorUserId: user.Id,
                TargetUserId: user.Id,
                request.Username,
                Succeeded: false), cancellationToken);
            throw;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var sessionId = Guid.NewGuid();
        var now = timeProvider.GetUtcNow();
        var access = tokenIssuer.IssueAccessToken(user, sessionId);

        await publisher.Publish(new AuditEventOccurredNotification(
            AuditAction.LoginSucceeded,
            ActorUserId: user.Id,
            TargetUserId: user.Id,
            request.Username), cancellationToken);

        return Result.Success(new AuthToken(
            access.Token,
            (access.ExpiresAt - now).TotalSeconds));
    }
}
