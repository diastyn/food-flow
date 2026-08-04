using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public sealed record Currency
{
    private Currency(string code, int decimalPlaces)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException("Currency code cannot be null or whitespace.");
        }

        if (decimalPlaces is < 0 or > 4)
        {
            throw new DomainException("Decimal places must be between 0 and 4.");
        }

        Code = code.ToUpperInvariant();
        DecimalPlaces = decimalPlaces;
    }

    public string Code { get; }
    
    public int DecimalPlaces { get; }

    public static readonly Currency Kzt = new("KZT", 2);
    public static readonly Currency Eur = new("EUR", 2);
    public static readonly Currency Usd = new("USD", 2);

    public static readonly IReadOnlyList<Currency> All = [Kzt, Eur, Usd];
    
    private static readonly IReadOnlyDictionary<string, Currency> Values =
        new Dictionary<string, Currency>
        {
            ["KZT"] = Kzt,
            ["USD"] = Usd,
            ["EUR"] = Eur
        };

    public static Currency FromCode(string code)
    {
        var normalized = code.Trim().ToUpperInvariant();
        return Values.TryGetValue(normalized, out var currency) ? currency 
            : throw new DomainException($"Unsupported currency code: '{code}'.");
    }

    public override string ToString() => Code;
}