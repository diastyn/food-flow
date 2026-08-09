using FoodFlow.API.Security;
using FoodFlow.BuildingBlocks.Domain.Mapper;
using FoodFlow.BuildingBlocks.Infrastructure;
using FoodFlow.Modules.Identity.Application.Configuration;
using FoodFlow.Modules.Identity.Infrastructure.Configuration;
using FoodFlow.Modules.Identity.Infrastructure.Persistence.Extensions;
using FoodFlow.Modules.Ordering.Application.Configuration;
using FoodFlow.Modules.Ordering.Infrastructure.Configuration;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .RegisterTimeProvider()
    .RegisterHttpRequestContext()
    .RegisterIdentityInfrastructureLayerServices(builder.Configuration)
    .RegisterIdentityApplicationLayerServices()
    .RegisterOrderingApplicationLayerServices()
    .RegisterOrderingInfrastructureLayerServices(builder.Configuration);

builder.Services.RegisterAutoMapper(
    FoodFlow.Modules.Identity.Domain.AssemblyReference.Assembly);

builder.Services.AddIdentityAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    await app.MigrateOrderingDatabaseAsync();
    await app.MigrateIdentityDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
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
