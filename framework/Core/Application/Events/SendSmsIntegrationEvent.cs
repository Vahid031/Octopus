using Octopus.Core.Contract.Events;

namespace Octopus.Core.Application.Events;

public class SendSmsIntegrationEvent : IntegrationEvent
{
    public string Message { get; }
    public string PhoneNumber { get; }

    public SendSmsIntegrationEvent(string sender, string message, string phoneNumber)
        : base(Guid.NewGuid(), sender)
    {
        Message = message;
        PhoneNumber = phoneNumber;
    }
}
