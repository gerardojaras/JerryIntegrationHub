using FluentAssertions;
using JerryIntegrationHub.Exceptions;

namespace JerryIntegrationHub.Tests;

public class CustomerAlreadyExistsExceptionTests
{
    [Fact]
    public void Contructor_Should_Set_Code()
    {
        var exception = new CustomerAlreadyExistsException("CUST001");
        
        exception.Code.Should().Be("CUSTOMER_ALREADY_EXISTS");
    }
}