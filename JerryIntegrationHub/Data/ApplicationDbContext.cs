using JerryIntegrationHub.Models;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasIndex(x => x.CustomerCode).IsUnique();
    }

    public DbSet<Customer> Customers => Set<Customer>();
}