namespace Modules.Common.Domain.Abstractions;

public class Entity<TId> where TId : struct
{
    public TId Id { get; set; }

    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity() { } // for EF
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
