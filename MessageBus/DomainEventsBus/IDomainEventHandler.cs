using Domain;

namespace MessageBus.DomainEventsBus;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    void Handle(IDomainEvent domainEvent);
}