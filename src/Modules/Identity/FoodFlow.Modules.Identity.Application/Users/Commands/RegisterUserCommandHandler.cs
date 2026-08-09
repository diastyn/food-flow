using FluentValidation;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Errors;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Users.Commands;

public sealed class RegisterUserCommandHandler(
    IValidator<RegisterUserCommand> validator,
    IPasswordHasher passwordHasher,
    [FromKeyedServices(nameof(Identity))] IUnitOfWork unitOfWork,
    IUserStore userStore)
    : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Guid>(UsersErrors.Application.ValidationError.New(errorMessage));
        }

        var user = User.Register(
            Username.Create(request.Username),
            Email.Create(request.Email),
            PasswordHash.FromHash(passwordHasher.Hash(request.Password)),
            PersonName.Create(request.Name.Firstname, request.Name.Lastname, request.Name.Fullname),
            string.IsNullOrEmpty(request.Phone) ? null : PhoneNumber.Create(request.Phone));

        await userStore.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success<Guid>(user.Id);
    }
}
