using Domain.Transaction;
using MessageBus.DomainEventsBus;
using Services.TransactionStories;

namespace Persistence.Transactions;

public class TransactionRepository: ITransactions
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly EFWriteDataContext _dbContext;

    public TransactionRepository(
        IDomainEventDispatcher dispatcher,
        EFWriteDataContext dbContext)
    {
        _dispatcher = dispatcher;
        _dbContext = dbContext;
    }
    
    public void Add(Transaction transaction)
    {
        _dispatcher.Dispatch(transaction.NewEvents);
        transaction.ClearEvents();
        _dbContext.Set<Transaction>().Add(transaction);
    }

    public Transaction? FindById(TransactionId id)
        => _dbContext.Set<Transaction>().FirstOrDefault(_ => _.Id == id);

    public IEnumerable<Transaction> All()
        => _dbContext.Set<Transaction>();

    public void Update(Transaction draft)
    {
        _dispatcher.Dispatch(draft.NewEvents);
        draft.ClearEvents();
    }
}