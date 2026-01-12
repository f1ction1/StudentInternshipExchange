namespace InternshipEx.Modules.Practices.Contracts.IntergrationEvents
{
    public record InternshipAddedToFavoriteIntegrationEvent(Guid EventId, Guid InternshipId, Guid StudentId, DateTime TimeStamp);
}
