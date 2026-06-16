namespace Appointment.Domain.Common ; 

public abstract class Entity 
{
    private readonly List<DomainEvent> _domainEvents = new();
    public Guid Id { get; }

    protected Entity()
    {}
    protected Entity(Guid id)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
    }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
