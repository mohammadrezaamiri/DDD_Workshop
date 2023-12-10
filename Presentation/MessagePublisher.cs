using MessageBus;

namespace Presentation;

public interface IDispatcher
{
    void Dispatch<TMessage>(TMessage message);
}

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch<TMessage>(TMessage message)
    {
        var handlers = 
            _serviceProvider.GetServices<IMessageHandler<TMessage>>();
        
        foreach (var handler in handlers)
            handler.Handle(message);
    }
}