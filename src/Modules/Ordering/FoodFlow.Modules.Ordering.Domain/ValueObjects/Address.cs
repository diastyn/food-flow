using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.Errors;

namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public sealed record Address
{
    private Address(string country, string city, string street, string? postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string? PostalCode { get; }

    public static Address Create(
        string country,
        string city,
        string street,
        string? postalCode)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException(AppErrors.Domain.CountryCannotBeEmpty.New());
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DomainException(AppErrors.Domain.CityCannotBeEmpty.New());
        }

        if (string.IsNullOrWhiteSpace(street))
        {
            throw new DomainException(AppErrors.Domain.StreetCannotBeEmpty.New());
        }

        return new Address(
            country.Trim(),
            city.Trim(),
            street.Trim(),
            string.IsNullOrWhiteSpace(postalCode) ? null : postalCode.Trim());
    }

    public override string ToString() => $"{Country}, {City}, {Street}, {PostalCode}";
}
