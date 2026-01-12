using InternshipEx.Modules.Applications.Contracts.IntergrationEvents;
using IntershipEx.Modules.Applications.Domain.Applications.Events;
using MassTransit;

namespace IntershipEx.Modules.Applications.Application.Events
{
    public class ApplicationAppliedDomainEventConsumer(
        IPublishEndpoint publishEndpoint) : IConsumer<ApplicationAppliedDomainEvent>
    {
        public async Task Consume(ConsumeContext<ApplicationAppliedDomainEvent> context)
        {
            var data = context.Message;
            await publishEndpoint.Publish(
                new ApplicationAppliedIntegrationEvent(data.EventId, data.InternshipId, data.StudentId, data.TimeStamp));
        }
    }
}
