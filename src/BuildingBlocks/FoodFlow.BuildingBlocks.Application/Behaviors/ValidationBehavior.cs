using FluentValidation;
using FoodFlow.BuildingBlocks.Domain.Errors;
using MediatR;
using ValidationException = FoodFlow.BuildingBlocks.Domain.Primitives.ValidationException;

namespace FoodFlow.BuildingBlocks.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(validators
            .Select(v => v
                .ValidateAsync(context, cancellationToken)));

        var errors = results
            .SelectMany(r => r.Errors)
            .Where(r => r is not null)
            .ToArray();

        var errorMessages = errors
            .Select(error => error.ErrorMessage)
            .ToArray();

        var properties = errors
            .Select(error => error.PropertyName)
            .ToArray();

        if (errorMessages.Length != 0)
        {
            throw new ValidationException(AppErrors.Application.Validation.New(errorMessages), properties);
        }

        return await next(cancellationToken);
    }
}
