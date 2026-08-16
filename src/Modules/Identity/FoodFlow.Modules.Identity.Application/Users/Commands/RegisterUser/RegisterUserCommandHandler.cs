using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Application.Audits.Notifications.AuditEventOccurred;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Specifications;
using FoodFlow.Modules.Identity.Domain.Entities.Audits.Enums;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(
    IPublisher publisher,
    IPasswordHasher passwordHasher,
    [FromKeyedServices(nameof(Identity))] IUnitOfWork unitOfWork,
    IUserStore userStore)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var byUsername = new UserSpecification()
            .ByUsername(request.Username);

        if (await userStore.ExistsAsync(byUsername, cancellationToken))
        {
            var error = AppErrors.Application.ExistsByUsername.New();
            await publisher.Publish(new AuditEventOccurredNotification(
                AuditAction.RegistrationFailed,
                ActorUserId: null,
                TargetUserId: null,
                request.Username,
                Details: error.Messages,
                Succeeded: false), cancellationToken);
            return Result.Failure<Guid>(error);
        }

        var byEmail = new UserSpecification()
            .ByEmail(request.Email);

        if (await userStore.ExistsAsync(byEmail, cancellationToken))
        {
            var error = AppErrors.Application.ExistsByEmail.New();
            await publisher.Publish(new AuditEventOccurredNotification(
                AuditAction.RegistrationFailed,
                ActorUserId: null,
                TargetUserId: null,
                request.Username,
                Details: error.Messages,
                Succeeded: false), cancellationToken);
            return Result.Failure<Guid>(error);
        }

        var user = User.Register(
            Username.Create(request.Username),
            Email.Create(request.Email),
            PasswordHash.FromHash(passwordHasher.Hash(request.Password)),
            PersonName.Create(request.Name.Firstname, request.Name.Lastname, request.Name.Fullname),
            string.IsNullOrEmpty(request.Phone) ? null : PhoneNumber.Create(request.Phone));

        _ = await userStore.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid>(user.Id);
    }
}
