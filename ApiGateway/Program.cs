using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationManager;
using GlobalErrorHandling;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional:false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
