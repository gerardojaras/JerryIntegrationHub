using System.ComponentModel.DataAnnotations;

namespace JerryIntegrationHub.Dtos;

public sealed class CreateWebhookEventRequest
{
    [Required]
    [MaxLength(100)]
    public string SourceSystem { get; init; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string EventType { get; init; } = string.Empty;
    [Required]
    public string Payload { get; init; } = string.Empty;
    
}