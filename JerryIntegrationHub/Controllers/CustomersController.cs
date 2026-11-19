using JerryIntegrationHub.Data;
using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Models;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JerryIntegrationHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService customerService): ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await customerService.GetAllAsync(cancellationToken);
        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await customerService.GetByIdAsync(id, cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> Create(CreateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await customerService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> Update(Guid id, UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await customerService.UpdateAsync(
            id,
            request,
            cancellationToken);
        
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await customerService.DeactivateAsync(id, cancellationToken);
        return success ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
    {
        var success = await customerService.ActivateAsync(id, cancellationToken);
        return success ? NoContent() : NotFound();
    }
}