using FluentValidation;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Application.Orders.Errors;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.Stores;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Ordering.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler(
    IOrderStore store,
    TimeProvider timeProvider,
    [FromKeyedServices(nameof(Ordering))] IUnitOfWork unitOfWork,
    IValidator<CreateOrderCommand> validator) : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            return Result.Failure<Guid>(OrderErrors.Application.ValidationError.New(errorMessage));
        }

        var now = timeProvider.GetUtcNow();
        var totalPrice = request.Items.Sum(item => item.UnitPrice * item.Quantity);

        var address = Address.Create(
            request.DeliveryAddress.Country,
            request.DeliveryAddress.City,
            request.DeliveryAddress.Street,
            request.DeliveryAddress.PostalCode);

        var order = Order.Create(
            new RestaurantId(request.RestaurantId),
            new CustomerId(request.CustomerId),
            now,
            totalPrice: Money.Create(totalPrice, Currency.Kzt),
            address,
            request.Items);

        _ = await store.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(order.Id.Value);
    }
}
