using Domain.Transaction;

namespace Services.TransactionStories;

public interface ITransactions
{
    void Add(Transaction transaction);
    Transaction? FindById(TransactionId id);
    IEnumerable<Transaction> All();
    void Update(Transaction draft);
}