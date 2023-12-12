using Domain;
using Domain.Account.Events;
using MessageBus.DomainEventsBus;

namespace Services;

public class AuditorService : IDomainEventHandler<AccountOpened>
{
    public void Handle(IDomainEvent @event)
        => Console.WriteLine(@event);
}

public class AnotherAuditorService : IDomainEventHandler<AccountOpened>
{
    public void Handle(IDomainEvent @event)
        => Console.WriteLine(@event);
}