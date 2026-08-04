using FluentValidation;

namespace FoodFlow.Modules.Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.RestaurantId).NotEmpty();
        RuleFor(x => x.DeliveryAddress).NotEmpty();
        
        // Инвариант: заказ не может быть пустым
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("The order must contain at least one product.")
            .Must(items => items.All(i => i.Quantity > 0))
            .WithMessage("The quantity of the product must be more than 0.");
    }
}