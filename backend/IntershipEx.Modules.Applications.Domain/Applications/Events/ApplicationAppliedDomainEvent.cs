
using Modules.Common.Domain.Abstractions;

namespace IntershipEx.Modules.Applications.Domain.Applications.Events
{
    public sealed record ApplicationAppliedDomainEvent(
        Guid EventId,
        Guid InternshipId,
        Guid StudentId,
        DateTime TimeStamp) : IDomainEvent;
}
