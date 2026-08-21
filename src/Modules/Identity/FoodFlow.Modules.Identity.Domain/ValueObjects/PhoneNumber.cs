using System.Text.RegularExpressions;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Errors;

namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

/// <summary>
/// Представляет номер телефона Республики Казахстан.
/// Номер хранится в нормализованном формате из 11 цифр,
/// начинающемся с кода страны 7.
/// </summary>
public sealed partial class PhoneNumber
{
    private PhoneNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Значение номера телефона в нормализованном формате.
    /// Например: 77071234567.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создает объект номера телефона и выполняет его валидацию.
    /// Поддерживает различные форматы ввода, например:
    /// +7 (707) 123-45-67,
    /// 8 (707) 123-45-67,
    /// 77071234567.
    /// </summary>
    /// <param name="value">Номер телефона.</param>
    /// <returns>Экземпляр <see cref="PhoneNumber"/>.</returns>
    /// <exception cref="DomainException">
    /// Выбрасывается, если номер телефона отсутствует или имеет некорректный формат.
    /// </exception>
    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(AppErrors.Domain.PhoneNumberIsRequired.New());
        }

        var phone = NonDigitRegex().Replace(value, string.Empty);

        if (phone.StartsWith('8'))
        {
            phone = "7" + phone[1..];
        }

        if (!KazakhstanPhoneNumberRegex().IsMatch(phone))
        {
            throw new DomainException(AppErrors.Domain.PhoneNumberIsInvalid.New());
        }

        return new PhoneNumber(phone);
    }

    /// <inheritdoc/>
    public override string ToString() => Value;

    /// <summary>
    /// Возвращает регулярное выражение для проверки номера телефона Казахстана.
    /// </summary>
    /// <returns>Скомпилированное регулярное выражение.</returns>
    [GeneratedRegex(@"^7\d{10}$")]
    private static partial Regex KazakhstanPhoneNumberRegex();

    /// <summary>
    /// Возвращает регулярное выражение для удаления всех символов,
    /// кроме цифр.
    /// </summary>
    /// <returns>Скомпилированное регулярное выражение.</returns>
    [GeneratedRegex(@"\D")]
    private static partial Regex NonDigitRegex();

    public static implicit operator string?(PhoneNumber? phoneNumber) => phoneNumber?.Value;

    public static implicit operator PhoneNumber(string phone) => Create(phone);
}
