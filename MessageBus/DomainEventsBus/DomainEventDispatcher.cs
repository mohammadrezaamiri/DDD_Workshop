using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus.DomainEventsBus;

public interface IDomainEventDispatcher
{
    void Dispatch<TEvent>(IEnumerable<TEvent> messages) where TEvent : IDomainEvent;
}

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch<TEvent>(IEnumerable<TEvent> messages) where TEvent : IDomainEvent
    {
        foreach (var message in messages)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(message.GetType());
            dynamic handlers = _serviceProvider.GetServices(handlerType);
            foreach (var handler in handlers)
                handler.Handle((dynamic)message);
        }
    }
}