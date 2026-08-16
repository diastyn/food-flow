using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Errors;

namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

/// <summary>
/// Уникальное имя пользователя (логин). Нормализуется к нижнему регистру.
/// Длина должна быть от <see cref="MinLength"/> до <see cref="MaxLength"/> символов.
/// </summary>
public sealed class Username
{
    /// <summary>Минимальная допустимая длина имени пользователя.</summary>
    public const int MinLength = 3;

    /// <summary>Максимальная допустимая длина имени пользователя.</summary>
    public const int MaxLength = 64;

    private Username(string value)
    {
        Value = value;
    }

    /// <summary>Нормализованное значение имени пользователя (нижний регистр).</summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект-значение <see cref="Username"/> из строки.
    /// Входная строка обрезается и приводится к нижнему регистру.
    /// </summary>
    /// <param name="value">Имя пользователя.</param>
    /// <returns>Новый экземпляр <see cref="Username"/>.</returns>
    /// <exception cref="DomainException">
    /// Выбрасывается, если значение пустое или не укладывается в допустимый диапазон длины.
    /// </exception>
    public static Username Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(AppErrors.Domain.UsernameCannotBeEmpty.New());
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length is < MinLength or > MaxLength)
        {
            throw new DomainException(AppErrors.Domain.UsernameLengthIsInvalid.New(MinLength, MaxLength));
        }

        return new Username(normalized);
    }

    /// <inheritdoc/>
    public override string ToString() => Value;

    public static implicit operator string(Username username) => username.Value;

    public static implicit operator Username(string username) => Create(username);
}
