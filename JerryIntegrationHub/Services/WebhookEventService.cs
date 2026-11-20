using JerryIntegrationHub.Data;
using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Models;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JerryIntegrationHub.Services;

public class WebhookEventService(ApplicationDbContext dbContext): IWebhookEventService
{
    public async Task<WebhookEventResponse> CreateAsync(CreateWebhookEventRequest request, CancellationToken cancellationToken)
    {
        var webhookEvent = new WebhookEvent
        {
            Id = Guid.NewGuid(),
            SourceSystem = request.SourceSystem,
            EventType = request.EventType,
            Payload = request.Payload,
            ReceivedAtUtc = DateTime.UtcNow,
            Processed = false
        };
        
        dbContext.WebhookEvents.Add(webhookEvent);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new WebhookEventResponse
        {
            Id = webhookEvent.Id,
            SourceSystem = webhookEvent.SourceSystem,
            EventType = webhookEvent.EventType,
            ReceivedAtUtc = webhookEvent.ReceivedAtUtc,
            Processed = webhookEvent.Processed
        };
    }

    public async Task<IEnumerable<WebhookEventResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.WebhookEvents
            .AsNoTracking()
            .OrderByDescending(x => x.ReceivedAtUtc)
            .Select(x => new WebhookEventResponse
            {
                Id = x.Id,
                SourceSystem = x.SourceSystem,
                EventType = x.EventType,
                ReceivedAtUtc = x.ReceivedAtUtc,
                Processed = x.Processed
            })
            .ToListAsync(cancellationToken);
    }
}