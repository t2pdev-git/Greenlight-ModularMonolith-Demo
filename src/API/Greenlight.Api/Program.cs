using System.Reflection;
using Greenlight.Api.Extensions;
using Greenlight.Api.Middleware;
using Greenlight.Api.OpenTelemetry;
using Greenlight.Common.Application;
using Greenlight.Common.Infrastructure;
using Greenlight.Common.Infrastructure.Configuration;
using Greenlight.Common.Presentation.Endpoints;
using Greenlight.Modules.Initiatives.Infrastructure;
using Greenlight.Modules.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

Assembly[] moduleApplicationAssemblies = [
    Greenlight.Modules.Initiatives.Application.AssemblyReference.Assembly,
    Greenlight.Modules.Users.Application.AssemblyReference.Assembly];

builder.Services.AddApplication(moduleApplicationAssemblies);

string databaseConnectionString = 
    builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnectionString = 
    builder.Configuration.GetConnectionStringOrThrow("Cache");

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
        InitiativesModule.ConfigureConsumers
    ],
    databaseConnectionString,
    redisConnectionString);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddKeyCloak(keyCloakHealthUrl);

builder.Configuration.AddModuleConfiguration([
    "initiatives",
    "users"
]);

builder.Services.AddInitiativesModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

builder.Host.UseDefaultServiceProvider(config =>
{
    // This catches the "Captive Dependency Problem" in any environment, not just Development.
    // "Captive Dependency Problem": If a longer-lived service holds a shorter-lived service,
    // an exception will be thrown by the dependency injection container at the runtime
    // which is called the "Captive Dependency Problem".
    config.ValidateOnBuild = true;
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContext();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

public partial class Program;
