using JerryIntegrationHub.Models;

namespace JerryIntegrationHub.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Customer> CreateAsync(Customer customer);
}