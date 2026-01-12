using InternshipEx.Modules.Practices.Contracts.IntergrationEvents;
using InternshipEx.Recommendations.Domain.Interactions;
using InternshipEx.Recommendations.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Recommendations.Infrastructure.EventConsumers
{
    public class InternshipRemovedFromFavoriteIntegrationEventConsumer(
        RecDbContext dbContext) : IConsumer<InternshipRemovedFromFavoriteIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<InternshipRemovedFromFavoriteIntegrationEvent> context)
        {
            var data = context.Message;
            var existingInteraction = await dbContext.Interactions.FirstOrDefaultAsync(i => i.InternshipId == data.InternshipId
                    && i.UserId == data.StudentId && i.Type == InteractionType.Liked);
            if (existingInteraction is null)
            {
                return;
            }
            dbContext.Interactions.Remove(existingInteraction);
            await dbContext.SaveChangesAsync();
        }
    }
}


