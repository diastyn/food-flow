using System.ComponentModel.DataAnnotations;

namespace FoodFlow.BuildingBlocks.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; init; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; init; } = null!;

    public int AccessTokenLifetimeMinutes { get; init; } = 15;

    public int RefreshTokenLifetimeDays { get; init; } = 14;

    public string? PrivateKeyPem { get; init; }

    public string KeyId { get; init; } = "foodflow-identity-key";
}
