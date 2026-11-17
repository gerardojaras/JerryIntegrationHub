using Microsoft.AspNetCore.Mvc;

namespace JerryIntegrationHub.Controllers;

[ApiController]
[Route("/")]
public class HomeController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            ApplicationName = "Jerry Integration Hub",
            Status = "Online",
            Version = "0.1.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
        });
    }
}