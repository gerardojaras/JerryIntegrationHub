using JerryIntegrationHub.Models;

namespace JerryIntegrationHub.Services.Interfaces;

public interface IWebhookProcessor
{
    Task ProcessAsync(WebhookEvent webhookEvent, CancellationToken cancellationToken);
}