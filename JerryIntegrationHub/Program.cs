using JerryIntegrationHub.Data;
using JerryIntegrationHub.Middleware;
using JerryIntegrationHub.Services;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace JerryIntegrationHub;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IWebhookEventService, WebhookEventService>();

        builder.Services.AddControllers();
        
        Console.WriteLine($"PGHOST: {builder.Configuration["TEST_HOST"]}");
        Console.WriteLine($"PGDATABASE: {builder.Configuration["TEST_DB"]}");
        Console.WriteLine($"PGUSER: {builder.Configuration["TEST_USER"]}");

        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString =
                new NpgsqlConnectionStringBuilder
                {
                    Host = builder.Configuration["PGHOST"],
                    Port = int.Parse(builder.Configuration["PGPORT"]!),
                    Database = builder.Configuration["PGDATABASE"],
                    Username = builder.Configuration["PGUSER"],
                    Password = builder.Configuration["PGPASSWORD"]
                }.ConnectionString;
        }

        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString));

        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
        
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}