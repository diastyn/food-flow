namespace FoodFlow.API.Middlewares;

/// <summary>
///     Расширения для IApplicationBuilder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     Использует промежуточное ПО для обработки исключений приложения.
    /// </summary>
    /// <param name="builder">Сборщик приложения.</param>
    public static IApplicationBuilder UseApplicationExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _ = builder.UseMiddleware<ApplicationExceptionHandlingMiddleware>();
        return builder;
    }
}
