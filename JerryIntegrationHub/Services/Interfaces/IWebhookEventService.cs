using JerryIntegrationHub.Dtos;

namespace JerryIntegrationHub.Services.Interfaces;

public interface IWebhookEventService
{
    Task<WebhookEventResponse> CreateAsync(CreateWebhookEventRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<WebhookEventResponse>> GetAllAsync(CancellationToken cancellationToken);
}