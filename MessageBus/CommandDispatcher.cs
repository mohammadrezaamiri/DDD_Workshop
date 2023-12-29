using Microsoft.Extensions.DependencyInjection;

namespace MessageBus;

public interface ICommandDispatcher
{
    void Dispatch<TCommand>(TCommand message) where TCommand : ICommand;
}

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CommandDispatcher(
        IServiceProvider serviceProvider, 
        IUnitOfWork unitOfWork)
    {
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
    }

    public void Dispatch<TCommand>(TCommand message) where TCommand : ICommand
    {
        var handler =
            _serviceProvider.GetService<ICommandHandler<TCommand>>();

        try
        {
            _unitOfWork.Start();
            
            handler?.Handle(message);
            
            _unitOfWork.Finish();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}