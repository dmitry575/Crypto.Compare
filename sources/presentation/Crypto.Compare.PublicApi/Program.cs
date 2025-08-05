using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Crypto.Compare.PublicApi.Ioc;
using Crypto.Compare.PublicApi.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using ApiRequestValidationFilter = Crypto.Compare.PublicApi.Filters.ApiRequestValidationFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Configuration
    .AddEnvironmentVariables()
    .AddEnvironmentVariables("ASPNETCORE_");

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

// configuration logger
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration);
builder.Logging.AddLog4Net();
if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
}


// need to PostgresSQL for working DataTime 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services
    .AddOptions()
    .AddCors()
    .AddServices(builder.Configuration)
    .AddDatabase(builder.Configuration)
    .AddMemoryCache()
    ;
builder.Services
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Api Crypto.Compare",
            Description = "Web API for managing Crypto.Compare",
        });

        options.ExampleFilters();
        options.EnableAnnotations();
        options.DescribeAllParametersInCamelCase();
        options.UseInlineDefinitionsForEnums();
    });

builder.Services.AddSwaggerExamplesFromAssemblyOf(typeof(Program));


builder.Services
    .AddMvc(options =>
    {
        options.Filters.Add<ApiRequestValidationFilter>();
        options.EnableEndpointRouting = false;
    })

    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
    ;

var configuration = builder.Configuration;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

var configuredOrigins = app.Configuration["Cors:AllowedOrigins"];
var origins = Array.Empty<string>();
if (!string.IsNullOrEmpty(configuredOrigins))
{
    origins = configuredOrigins.Split(',', ';').Select(i => i.Trim()).ToArray();
}

app
    .UsePathBaseFromEnvironmentVariables()
    .UseAuthentication()
    .UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    )

    .UseRouting()
    .UseAuthorization()
    .UseMiddleware<RequestIdLoggingMiddleware>()
    .UseMiddleware<RequestResponseLoggingMiddleware>(
        new RequestResponseLoggingMiddlewareOptions
        {
            EnableRequestLogging = true,
            EnableResponseLogging = true
        })
    .UseMiddleware<UnhandledExceptionMiddleware>()
    .UseMiddleware<StoppingMiddleware>()
    .UseMiddleware<AbortRequestMiddleware>()
    .UseMiddleware<UserIdLoggingMiddleware>()
    .UseEndpoints(x => { x.MapControllers(); })
    ;

//app.UseHttpsRedirection();

app.Run();
