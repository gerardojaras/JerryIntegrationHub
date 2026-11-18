using JerryIntegrationHub.Data;
using JerryIntegrationHub.Models;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub.Services;

public class CustomerService(ApplicationDbContext context) : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Customer
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    
    public async Task<Customer> CreateAsync(Customer customer)
    {
        context.Customer.Add(customer);
        await context.SaveChangesAsync();
        return customer;
    }
}