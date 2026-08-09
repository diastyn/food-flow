using System.Net.Mail;
using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

/// <summary>
/// Адрес электронной почты пользователя.
/// Нормализуется к нижнему регистру и проверяется на корректность формата.
/// </summary>
public sealed class Email
{
    public const int MaxLength = 320;

    private Email(string value)
    {
        Value = value;
    }

    /// <summary>Нормализованное значение адреса электронной почты.</summary>
    public string Value { get; }

    /// <summary>
    /// Создаёт объект-значение <see cref="Email"/> из строки.
    /// Входная строка обрезается, приводится к нижнему регистру и проверяется на корректность.
    /// </summary>
    /// <param name="value">Адрес электронной почты.</param>
    /// <returns>Новый экземпляр <see cref="Email"/>.</returns>
    /// <exception cref="DomainException">
    /// Выбрасывается, если значение пустое или не является корректным адресом e-mail.
    /// </exception>
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email cannot be empty.");
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (!MailAddress.TryCreate(normalized, out _))
        {
            throw new DomainException($"'{value}' is not a valid email address.");
        }

        return new Email(normalized);
    }

    /// <inheritdoc/>
    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string email) => Create(email);
}
