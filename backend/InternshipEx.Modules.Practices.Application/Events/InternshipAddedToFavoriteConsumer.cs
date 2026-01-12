using InternshipEx.Modules.Practices.Contracts.IntergrationEvents;
using InternshipEx.Modules.Practices.Domain.Entities.InternshipFavorites.Events;
using MassTransit;

namespace InternshipEx.Modules.Practices.Application.Events
{
    public class InternshipAddedToFavoriteConsumer(IPublishEndpoint publishEndpoint) : IConsumer<InternshipAddedToFavorite>
    {
        public async Task Consume(ConsumeContext<InternshipAddedToFavorite> context)
        {
            await publishEndpoint.Publish(new InternshipAddedToFavoriteIntegrationEvent(
                context.Message.EventId, context.Message.InternshipId, context.Message.StudentId, context.Message.TimeStamp));
        }
    }
}
