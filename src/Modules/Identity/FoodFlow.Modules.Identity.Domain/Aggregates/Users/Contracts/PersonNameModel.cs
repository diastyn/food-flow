namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;

public class PersonNameModel
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Firstname { get; init; } = null!;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string Lastname { get; init; } = null!;

    /// <summary>
    /// Полное имя пользователя. По умолчанию формируется как «Имя Фамилия».
    /// </summary>
    public string? Fullname { get; init; }
}
