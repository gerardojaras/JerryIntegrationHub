using JerryIntegrationHub.Dtos;
using JerryIntegrationHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JerryIntegrationHub.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookEventsController(IWebhookEventService webhookEventService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WebhookEventResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var webhookEvents = await webhookEventService.GetAllAsync(cancellationToken);
        return Ok(webhookEvents);
    }

    [HttpGet("{id: guid}")]
    public async Task<ActionResult<WebhookEventDetailResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var webhookEvent = await webhookEventService.GetDetailsAsync(id, cancellationToken);
        return webhookEvent is null ? NotFound() : Ok(webhookEvent);
    }

    [HttpPost]
    public async Task<ActionResult<WebhookEventResponse>> Create(
        CreateWebhookEventRequest request,
        CancellationToken cancellationToken)
    {
        var webhookEvents = await webhookEventService.CreateAsync(request, cancellationToken);
        return Created(string.Empty, webhookEvents);
    }
}