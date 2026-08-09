using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

/// <summary>
/// Имя пользователя в виде составного объекта-значения: имя, фамилия и полное имя.
/// Если <c>fullName</c> не задан явно — формируется как «Имя Фамилия».
/// </summary>
public sealed class PersonName
{
    private PersonName(string firstName, string lastName, string fullName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
    }

    /// <summary>Имя пользователя.</summary>
    public string FirstName { get; }

    /// <summary>Фамилия пользователя.</summary>
    public string LastName { get; }

    /// <summary>Полное имя пользователя. По умолчанию формируется как «Имя Фамилия».</summary>
    public string FullName { get; }

    /// <summary>
    /// Создаёт объект-значение <see cref="PersonName"/>.
    /// </summary>
    /// <param name="firstName">Имя (не должно быть пустым).</param>
    /// <param name="lastName">Фамилия (не должна быть пустой).</param>
    /// <param name="fullName">
    /// Полное имя. Если <c>null</c> или пустое — вычисляется как <c>firstName + " " + lastName</c>.
    /// </param>
    /// <returns>Новый экземпляр <see cref="PersonName"/>.</returns>
    /// <exception cref="DomainException">Выбрасывается, если имя или фамилия пустые.</exception>
    public static PersonName Create(
        string firstName,
        string lastName,
        string? fullName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainException("First name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException("Last name cannot be empty.");
        }

        var first = firstName.Trim();
        var last = lastName.Trim();
        var full = string.IsNullOrWhiteSpace(fullName) ? $"{first} {last}" : fullName.Trim();

        return new PersonName(first, last, full);
    }

    public override string ToString() => FullName;
}
