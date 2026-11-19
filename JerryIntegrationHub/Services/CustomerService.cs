using JerryIntegrationHub.Data;
using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Exceptions;
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
            .Where(x => x.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    
    public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customerExists = await dbContext.Customers.AnyAsync(
            x => x.CustomerCode == request.CustomerCode, cancellationToken);

        if (customerExists)
        {
            throw new CustomerAlreadyExistsException(request.CustomerCode);
        }
        
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

    public async Task<CustomerResponse?> UpdateAsync(Guid id, UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customer is null) return null;

        customer.CustomerCode = request.CustomerCode;
        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.IsActive = request.IsActive;

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

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customer is null) return false;
        customer.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customer is null) return false;
        
        customer.IsActive = true;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}