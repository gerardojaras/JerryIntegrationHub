namespace JerryIntegrationHub.Exceptions;

public class WebhookAlreadyProcessedException(
    Guid webhookEventId): 
    BusinessException("WEBHOOK_ALREADY_PROCESSED", $"Webhook event {webhookEventId} has already been processed.")
{
    public override int StatusCode => StatusCodes.Status409Conflict;
}