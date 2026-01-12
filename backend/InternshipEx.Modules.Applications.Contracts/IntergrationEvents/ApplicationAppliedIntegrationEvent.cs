namespace InternshipEx.Modules.Applications.Contracts.IntergrationEvents
{
    public record ApplicationAppliedIntegrationEvent(
        Guid EventId,
        Guid InternshipId,
        Guid StudentId,
        DateTime TimeStamp);
}
