using System.ComponentModel.DataAnnotations;

namespace FoodFlow.Modules.Identity.Infrastructure.Configuration;

internal sealed class AdminCredentialsOptions
{
    public const string SectionName = "AdminCredentials";

    [Required(AllowEmptyStrings = false)]
    public string Username { get; init; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; init; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Email { get; init; } = null!;

    public string Firstname { get; init; } = "Admin";

    public string Lastname { get; init; } = "Admin";
}
