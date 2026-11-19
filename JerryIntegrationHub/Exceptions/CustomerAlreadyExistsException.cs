namespace JerryIntegrationHub.Exceptions;

public sealed class CustomerAlreadyExistsException(string customerCode)
    : BusinessException("CUSTOMER_ALREADY_EXISTS",
        $"Customer with code {customerCode} already exists.")
{
    public override int StatusCode => StatusCodes.Status409Conflict;
}