namespace JerryIntegrationHub.Models;

public sealed class WebhookEvent
{
    public Guid Id { get; set; }
    public string SourceSystem { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime ReceivedAtUtc { get; set; } = DateTime.UtcNow;
    public bool Processed { get; set; }
}