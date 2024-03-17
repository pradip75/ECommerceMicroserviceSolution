
using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Interfaces;
using Order.Infrastructure.Repositories;
using JwtAuthenticationManager;
using Order.Application.EventConsumer;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var kafkaConfig = builder.Configuration.GetSection("Kafka");
var bootstrapServers = kafkaConfig.GetValue<string>("BootstrapServers");
builder.Services.AddHttpClient();

builder.Services.AddSingleton(provider =>
{
    return new ProductChangeEventConsumer(bootstrapServers, dependency);
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var kafkaConsumer = services.GetRequiredService<ProductChangeEventConsumer>();

    // Start consuming messages when the microservice starts up
    await kafkaConsumer.StartConsumingAsync();
}

// Hook into the application shutdown to stop consuming messages
app.RunAndBlockUntilShutdown();

await app.RunAsync();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class HostExtensions
{
    public static void RunAndBlockUntilShutdown(this IHost host)
    {
        var applicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

        // Register a callback for application shutdown
        applicationLifetime.ApplicationStopping.Register(() =>
        {
            // Get an instance of the ProductChangeEventConsumer
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var productChangeEventConsumer = services.GetRequiredService<ProductChangeEventConsumer>();

                // Stop consuming messages when the microservice shuts down
                productChangeEventConsumer.StopConsuming();
            }
        });

        host.Run();
    }
}