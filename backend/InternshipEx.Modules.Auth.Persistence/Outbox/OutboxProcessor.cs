using MassTransit;
using Microsoft.EntityFrameworkCore;
using InternshipEx.Modules.Auth.Domain.Helpers;
using System.Text.Json;
using InternshipEx.Modules.Auth.Domain.Primitives;
using Newtonsoft.Json;
using Internship.Modules.Auth.IntegrationEvents;
using InternshipEx.Modules.Auth.Domain.DomainEvents;


namespace InternshipEx.Modules.Auth.Persistence.Outbox
{
    public class OutboxProcessor(IPublishEndpoint publishEndpoint, AuthDbContext dbContext)
    {
        private const int BatchSize = 10;

        public async Task<int> Execute(CancellationToken cancellationToken = default)
        {
            var outboxMessages = await dbContext.OutboxMessages
                .Where(m => m.ProcessedOn == null)
                //.OrderBy(m => m.OccurredOn)
                .Take(BatchSize)
                .ToListAsync(cancellationToken); 
            // if it'll be more than one Processor running at the same time,
            // it should be in transaction with row locking

            foreach (var outboxMessage in outboxMessages)
            {
                try
                {
                    IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content,
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All});
                    if (domainEvent == null)
                    {
                        throw new InvalidOperationException($"Could not deserialize outbox message with id {outboxMessage.Id}");
                    }
                    await publishEndpoint.Publish(domainEvent, domainEvent.GetType(), cancellationToken);

                    outboxMessage.ProcessedOn = DateTime.UtcNow;
                } 
                catch (Exception e)
                {
                    outboxMessage.ProcessedOn = DateTime.UtcNow;
                    outboxMessage.Error = e.ToString();
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return outboxMessages.Count;
        }
    }
}
