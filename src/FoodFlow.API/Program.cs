using FoodFlow.BuildingBlocks.API.ApiVersioning;
using FoodFlow.BuildingBlocks.API.Middlewares;
using FoodFlow.BuildingBlocks.API.ModelConventions;
using FoodFlow.BuildingBlocks.API.OpenApi;
using FoodFlow.BuildingBlocks.Application;
using FoodFlow.BuildingBlocks.Authentication;
using FoodFlow.BuildingBlocks.Authorization;
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
    var configuration = builder.Configuration;

    _ = builder.Services
        .RegisterTimeProvider()
        .RegisterHttpRequestContext()
        .RegisterJwtAuthenticationDefaults(configuration)
        .RegisterIdentityInfrastructureLayerServices(configuration)
        .RegisterApplicationLayerDefaults(
            FoodFlow.Modules.Identity.Application.Configuration.AssemblyReference.Assembly,
            FoodFlow.Modules.Ordering.Application.Configuration.AssemblyReference.Assembly)
        .RegisterOrderingInfrastructureLayerServices(configuration)
        .RegisterMapper(
            FoodFlow.Modules.Identity.Domain.AssemblyReference.Assembly,
            FoodFlow.Modules.Ordering.Domain.AssemblyReference.Assembly);

    _ = builder.Services.AddIdentityAuthentication();
    _ = builder.Services.AddPermissionAuthorization();
    _ = builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseControllerRouteConvention()))
        .AddMvcOptions(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; })
        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
    _ = builder.Services.AddRestApiVersioning();

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
        _ = app.MapOpenApi().WithDocumentPerVersion();
        _ = app.UseScalarApiReference();
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
