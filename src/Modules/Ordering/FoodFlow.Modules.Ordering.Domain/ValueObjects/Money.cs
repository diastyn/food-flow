using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.Errors;

namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct Money
{
    public decimal Amount { get; }

    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);
        Currency = currency;
        Amount = decimal.Round(amount, currency.DecimalPlaces, MidpointRounding.ToEven);
    }

    public static Money Create(decimal amount, Currency currency) => new(amount, currency);

    public static Money Zero(Currency currency) => new(0, currency);

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal multiplier) => new(Amount * multiplier, Currency);

    private void EnsureSameCurrency(Money other)
    {
        if (Currency is null || other.Currency is null || !Currency.Equals(other.Currency))
        {
            throw new DomainException(AppErrors.Domain.MoneyCurrencyMismatch.New());
        }
    }

    // Operators
    public static Money operator +(Money left, Money right) => left.Add(right);

    public static Money operator -(Money left, Money right) => left.Subtract(right);

    public static Money operator *(Money left, decimal right) => left.Multiply(right);

    public static Money operator *(decimal left, Money right) => right.Multiply(left);
}
