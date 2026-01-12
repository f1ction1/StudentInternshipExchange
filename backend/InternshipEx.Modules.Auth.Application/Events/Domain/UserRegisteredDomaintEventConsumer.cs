using Internship.Modules.Auth.IntegrationEvents;
using InternshipEx.Modules.Auth.Domain.DomainEvents;
using MassTransit;

namespace InternshipEx.Modules.Auth.Application.Events.Domain
{
    public class UserRegisteredDomaintEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<UserRegisteredDomainEvent>
    {
        public async Task Consume(ConsumeContext<UserRegisteredDomainEvent> context)
        {
            await publishEndpoint.Publish(new UserRegisteredIntegrationEvent(context.Message.UserId, context.Message.Role));
        }
    }
}
