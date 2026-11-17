using Microsoft.AspNetCore.Mvc;

namespace JerryIntegrationHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            ApplicationName = "Jerry Integration Hub",
            Status = "Online",
            Version = "0.1.0",
            Timestamp = DateTime.UtcNow,
        });
    }
}