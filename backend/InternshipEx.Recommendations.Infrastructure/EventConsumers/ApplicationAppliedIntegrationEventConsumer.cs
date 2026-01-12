using InternshipEx.Modules.Applications.Contracts.IntergrationEvents;
using InternshipEx.Recommendations.Domain.Interactions;
using InternshipEx.Recommendations.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Recommendations.Infrastructure.EventConsumers
{
    public class ApplicationAppliedIntegrationEventConsumer(
        RecDbContext dbContext) : IConsumer<ApplicationAppliedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ApplicationAppliedIntegrationEvent> context)
        {
            var data = context.Message;
            var existingInteraction = await dbContext.Interactions.FirstOrDefaultAsync(i => i.EventId != null && i.EventId == data.EventId);
            if(existingInteraction != null)
            {
                return;
            }
            var interaction = Interaction.Create(
                eventId: data.EventId,
                internshipId: data.InternshipId,
                userId: data.StudentId,
                type: InteractionType.Applied,
                timeStamp: data.TimeStamp);
            await dbContext.Interactions.AddAsync(interaction);
            await dbContext.SaveChangesAsync();
        }
    }
}
