using JerryIntegrationHub.Data;
using JerryIntegrationHub.Middleware;
using JerryIntegrationHub.Services;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<ICustomerService, CustomerService>();

        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();
        
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}