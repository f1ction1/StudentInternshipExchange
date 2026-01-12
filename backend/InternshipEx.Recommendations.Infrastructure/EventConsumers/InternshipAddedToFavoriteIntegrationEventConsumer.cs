using InternshipEx.Modules.Practices.Contracts.IntergrationEvents;
using InternshipEx.Recommendations.Domain.Interactions;
using InternshipEx.Recommendations.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Recommendations.Infrastructure.EventConsumers
{
    public class InternshipAddedToFavoriteIntegrationEventConsumer(
        RecDbContext dbContext) : IConsumer<InternshipAddedToFavoriteIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<InternshipAddedToFavoriteIntegrationEvent> context)
        {
            var data = context.Message;
            var existingInteraction = await dbContext.Interactions.FirstOrDefaultAsync(i => i.EventId != null && i.EventId == data.EventId);
            if(existingInteraction is not null)
            {
                return;
            }

            var interaction = Interaction.Create(data.StudentId, data.InternshipId, InteractionType.Liked, data.TimeStamp, data.EventId);
            await dbContext.Interactions.AddAsync(interaction);
            await dbContext.SaveChangesAsync();
        }
    }
}
