namespace MessageBus;

public interface IMessageHandler<in TMessage>
{
    void Handle(TMessage message);
}