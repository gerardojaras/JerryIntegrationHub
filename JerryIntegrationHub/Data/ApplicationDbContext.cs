using JerryIntegrationHub.Models;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {}

    public DbSet<Customer> Customer => Set<Customer>();
}