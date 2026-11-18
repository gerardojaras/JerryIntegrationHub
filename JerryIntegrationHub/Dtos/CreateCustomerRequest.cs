using System.ComponentModel.DataAnnotations;

namespace JerryIntegrationHub.Dtos;

public class CreateCustomerRequest
{
    [Required]
    [MaxLength(20)]
    public string CustomerCode { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;
}