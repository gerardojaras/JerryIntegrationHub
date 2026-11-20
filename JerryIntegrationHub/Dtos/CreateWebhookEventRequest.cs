using System.ComponentModel.DataAnnotations;

namespace JerryIntegrationHub.Dtos;

public sealed class CreateWebhookEventRequest
{
    [Required]
    [MaxLength(100)]
    public string SourceSystem { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string EventType { get; set; } = string.Empty;
    [Required]
    public string Payload { get; set; } = string.Empty;
    
}