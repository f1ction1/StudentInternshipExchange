using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Domain.Applications.Events
{
    public record ApplicationRejectedDomainEvent(Guid eventId, Guid ApplicationId, Guid StudentId, Guid InternshipId ,string? reason) : IDomainEvent;
}
