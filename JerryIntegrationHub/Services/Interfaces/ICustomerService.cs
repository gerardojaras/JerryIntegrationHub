using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Models;

namespace JerryIntegrationHub.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task<CustomerResponse> CreateAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken);
    Task<CustomerResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}