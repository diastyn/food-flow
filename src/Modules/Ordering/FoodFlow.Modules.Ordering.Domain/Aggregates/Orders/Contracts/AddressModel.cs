namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public sealed class AddressModel
{
    public string Street { get; init; } = null!;

    public string City { get; init; } = null!;

    public string Country { get; init; } = null!;

    public string? PostalCode { get; init; }
}
