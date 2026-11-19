namespace JerryIntegrationHub.Exceptions;

public abstract class BusinessException(string code, string message) : Exception(message)
{
    public string Code { get; } = code;
    public abstract int StatusCode { get; }
}