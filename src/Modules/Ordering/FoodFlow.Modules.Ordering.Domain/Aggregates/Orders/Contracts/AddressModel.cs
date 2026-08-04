namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public record AddressModel(
    string Street, 
    string City, 
    string Country, 
    string? PostalCode);