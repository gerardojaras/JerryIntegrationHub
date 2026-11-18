using JerryIntegrationHub.Data;
using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Models;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub.Services;

public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    
    public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerCode = request.CustomerCode,
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
        };
        
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new CustomerResponse
        {
            Id = customer.Id,
            CustomerCode = customer.CustomerCode,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            IsActive = customer.IsActive,
            CreatedUtc = customer.CreatedAtUtc,
        };
    }

    public async Task<CustomerResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Customers
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(x => new CustomerResponse
            {
                Id = x.Id,
                CustomerCode = x.CustomerCode,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive,
                CreatedUtc =  x.CreatedAtUtc,
            }).FirstOrDefaultAsync(cancellationToken);
    }
}