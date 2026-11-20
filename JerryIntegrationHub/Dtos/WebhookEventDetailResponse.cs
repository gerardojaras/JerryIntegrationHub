namespace JerryIntegrationHub.Dtos;

public sealed class WebhookEventDetailResponse
{
    public Guid Id { get; init; }
    public string SourceSystem { get; init; } = string.Empty;
    public string EventType { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public DateTime ReceivedAtUtc { get; init; }
    public bool Processed { get; init; }
}