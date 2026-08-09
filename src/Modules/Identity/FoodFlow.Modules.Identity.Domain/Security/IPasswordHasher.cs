namespace FoodFlow.Modules.Identity.Domain.Security;

public interface IPasswordHasher
{
    /// <summary>
    /// Хеширует пароль с уникальной солью.
    /// Результат можно безопасно хранить в базе данных.
    /// </summary>
    /// <param name="password">Пароль в открытом виде.</param>
    /// <returns>Строка хеша в формате <c>итерации.соль.хеш</c> (Base64).</returns>
    public string Hash(string password);

    /// <summary>
    /// Проверяет соответствие пароля в открытом виде ранее сохранённому хешу.
    /// Использует постоянное по времени сравнение для защиты от атак по времени.
    /// </summary>
    /// <param name="password">Пароль в открытом виде для проверки.</param>
    /// <param name="hash">Ранее сохранённый хеш пароля.</param>
    /// <returns><c>true</c>, если пароль совпадает с хешем.</returns>
    public bool Verify(string password, string hash);
}
