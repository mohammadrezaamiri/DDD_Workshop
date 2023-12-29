using Domain.Transaction;

namespace Services.TransactionStories;

public interface ITransactions
{
    void Add(Transaction transaction);
    Transaction? FindById(TransactionId id);
    void Update(Transaction draft);
}