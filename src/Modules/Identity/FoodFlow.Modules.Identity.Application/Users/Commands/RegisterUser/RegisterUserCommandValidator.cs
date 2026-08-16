using FluentValidation;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        _ = RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("Username is required.");

        _ = RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password is required.");

        _ = RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email is required.");

        _ = RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        _ = RuleFor(u => u.Name.Firstname)
            .NotEmpty()
            .WithMessage("First name is required.");

        _ = RuleFor(u => u.Name.Lastname)
            .NotEmpty()
            .WithMessage("Last name is required.");
    }
}
