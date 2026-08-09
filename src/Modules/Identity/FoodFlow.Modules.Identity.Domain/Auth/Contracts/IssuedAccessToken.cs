namespace FoodFlow.Modules.Identity.Domain.Auth.Contracts;

/// <summary>
/// Выданный access token с датой и временем истечения срока действия.
/// </summary>
/// <param name="Token">Строковое представление токена.</param>
/// <param name="ExpiresAt">Дата и время истечения срока действия токена.</param>
public sealed record IssuedAccessToken(string Token, DateTimeOffset ExpiresAt);
