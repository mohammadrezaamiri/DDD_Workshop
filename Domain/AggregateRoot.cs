namespace Domain;

public class AggregateRoot
{
    protected Queue<IDomainEvent> newEvents = new();
    public IEnumerable<IDomainEvent> NewEvents => newEvents;
    public void ClearEvents()
        => newEvents.Clear();
}