using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Auth.Contracts;

namespace FoodFlow.Modules.Identity.Domain.Auth;

/// <summary>
/// Сервис выпуска JWT access-токенов.
/// Используется при аутентификации и обновлении токенов.
/// </summary>
public interface IJwtTokenIssuer
{
    /// <summary>
    /// Выпускает подписанный JWT access-токен для указанного пользователя.
    /// Токен содержит claims: <c>sub</c>, <c>preferred_username</c>, <c>email</c>,
    /// <c>name</c>, <c>sid</c> (идентификатор сессии), а также все назначенные роли и разрешения.
    /// </summary>
    /// <param name="user">Пользователь, для которого выпускается токен.</param>
    /// <param name="sessionId">Идентификатор сессии, к которой привязан токен.</param>
    /// <returns>Выпущенный токен с датой истечения срока действия.</returns>
    public IssuedAccessToken IssueAccessToken(User user, Guid sessionId);
}
