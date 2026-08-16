using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Errors;

namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

/// <summary>
/// Обёртка над хешем пароля.
/// Хеширование и проверка пароля — ответственность инфраструктуры (<c>IPasswordHasher</c>);
/// домен только хранит и передаёт непрозрачный хеш.
/// </summary>
public sealed class PasswordHash
{
    private PasswordHash(string value)
    {
        Value = value;
    }

    /// <summary>Строковое представление хеша (формат PBKDF2: итерации.соль.хеш в Base64).</summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект-значение <see cref="PasswordHash"/> из готового хеша,
    /// сформированного инфраструктурным хешером.
    /// </summary>
    /// <param name="hash">Строка хеша пароля (не должна быть пустой).</param>
    /// <returns>Новый экземпляр <see cref="PasswordHash"/>.</returns>
    /// <exception cref="DomainException">Выбрасывается, если хеш пустой.</exception>
    public static PasswordHash FromHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
        {
            throw new DomainException(AppErrors.Domain.PasswordHashCannotBeEmpty.New());
        }

        return new PasswordHash(hash);
    }

    /// <inheritdoc/>
    public override string ToString() => Value;
}
