using FoodFlow.API.Middlewares;
using FoodFlow.API.Security;
using FoodFlow.BuildingBlocks.Application;
using FoodFlow.BuildingBlocks.Domain.Mapper;
using FoodFlow.BuildingBlocks.Infrastructure;
using FoodFlow.Modules.Identity.Infrastructure.Configuration;
using FoodFlow.Modules.Identity.Infrastructure.Persistence.Extensions;
using FoodFlow.Modules.Ordering.Infrastructure.Configuration;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    _ = builder.Services.AddOpenApi();

    _ = builder.Services
        .RegisterTimeProvider()
        .RegisterHttpRequestContext()
        .RegisterIdentityInfrastructureLayerServices(builder.Configuration)
        .RegisterApplicationLayerDefaults(
            FoodFlow.Modules.Identity.Application.Configuration.AssemblyReference.Assembly,
            FoodFlow.Modules.Ordering.Application.Configuration.AssemblyReference.Assembly)
        .RegisterOrderingInfrastructureLayerServices(builder.Configuration)
        .RegisterMapper(
            FoodFlow.Modules.Identity.Domain.AssemblyReference.Assembly,
            FoodFlow.Modules.Ordering.Domain.AssemblyReference.Assembly);

    _ = builder.Services.AddIdentityAuthentication();
    _ = builder.Services.AddPermissionAuthorization();
    _ = builder.Services.AddControllers()
        .AddMvcOptions(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; })
        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

    _ = builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownIPNetworks.Clear();
        options.KnownProxies.Clear();
    });

    var app = builder.Build();

    _ = app.UseForwardedHeaders();
    _ = app.UseApplicationExceptionHandlerMiddleware();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        _ = app.MapOpenApi();
        await app.MigrateOrderingDatabaseAsync();
        await app.MigrateIdentityDatabaseAsync();
    }

    _ = app.UseHttpsRedirection();

    _ = app.UseAuthentication();
    _ = app.UseAuthorization();
    _ = app.MapControllers();

    Log.Information("Starting web host.");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    await Log.CloseAndFlushAsync();
}
