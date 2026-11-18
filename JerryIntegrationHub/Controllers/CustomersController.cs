using JerryIntegrationHub.Data;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JerryIntegrationHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService _customerService): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _customerService.GetAllAsync(cancellationToken);
        return Ok(customers);
    }
}