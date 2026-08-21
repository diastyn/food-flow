namespace FoodFlow.Modules.Identity.Presentation.Contracts;

public sealed record GrantPermissionsRequest(List<string> Permissions);
