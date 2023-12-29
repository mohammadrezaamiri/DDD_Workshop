namespace MessageBus;

public interface IUnitOfWork
{
    void Start();
    void Finish();
    void Rollback();
}