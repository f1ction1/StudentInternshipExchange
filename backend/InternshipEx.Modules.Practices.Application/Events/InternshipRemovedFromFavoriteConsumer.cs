using InternshipEx.Modules.Practices.Contracts.IntergrationEvents;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events;
using MassTransit;

namespace InternshipEx.Modules.Practices.Application.Events
{
    public class InternshipRemovedFromFavoriteConsumer(IPublishEndpoint publishEndpoint) : IConsumer<InternshipRemovedFromFavorite>
    {
        public async Task Consume(ConsumeContext<InternshipRemovedFromFavorite> context)
        {
            await publishEndpoint.Publish(new InternshipRemovedFromFavoriteIntegrationEvent(
                context.Message.EventId, context.Message.InternshipId, context.Message.StudentId));
        }
    }
}
