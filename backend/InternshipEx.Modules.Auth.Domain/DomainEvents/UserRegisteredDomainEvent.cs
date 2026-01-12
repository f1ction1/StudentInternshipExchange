using InternshipEx.Modules.Auth.Domain.Primitives;

namespace InternshipEx.Modules.Auth.Domain.DomainEvents
{
    public sealed record UserRegisteredDomainEvent(Guid UserId, string Role) : IDomainEvent
    {
    }
}
