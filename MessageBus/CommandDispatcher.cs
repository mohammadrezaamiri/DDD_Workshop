using Microsoft.Extensions.DependencyInjection;

namespace MessageBus;

public interface ICommandDispatcher
{
    void Dispatch<TCommand>(TCommand message) where TCommand : ICommand;
}

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispatch<TCommand>(TCommand message) where TCommand : ICommand
    {
        var handlers = 
            _serviceProvider.GetServices<ICommandHandler<TCommand>>();
        
        foreach (var handler in handlers)
            handler.Handle(message);
    }
}