namespace FoodFlow.Modules.Identity.Domain.Auth.Contracts;

/// <summary>
/// Пара токенов, возвращаемая клиенту после аутентификации или обновления токена.
/// </summary>
/// <param name="AccessToken">JWT-токен доступа.</param>
/// <param name="ExpiresIn">Время жизни access token в секундах.</param>
public sealed record AuthToken(
    string AccessToken,
    double ExpiresIn);
