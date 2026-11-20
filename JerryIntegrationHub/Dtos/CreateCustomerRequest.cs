using System.ComponentModel.DataAnnotations;

namespace JerryIntegrationHub.Dtos;

public class CreateCustomerRequest
{
    [Required]
    [MaxLength(20)]
    public string CustomerCode { get; init; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;
    [EmailAddress]
    [MaxLength(250)]
    public string Email { get; init; } = string.Empty;
    [MaxLength(50)]
    public string PhoneNumber { get; init; } = string.Empty;
}