using JerryIntegrationHub.Models;
using JerryIntegrationHub.Services.Interfaces;

namespace JerryIntegrationHub.Services;

public class WebhookProcessor : IWebhookProcessor
{
    public Task ProcessAsync(WebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Processing webhook from {webhookEvent.SourceSystem} - {webhookEvent.EventType}");
        
        return Task.CompletedTask;
    }
}