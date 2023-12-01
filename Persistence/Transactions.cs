using Domain.Transaction;
using Services;

namespace Persistence;

public class Transactions: ITransactions
{
    private readonly List<Transaction> _records = new();
    public void Add(Transaction transaction)
        => _records.Add(transaction);

    public Transaction? FindById(TransactionId id)
        => All().FirstOrDefault(tx => tx.Id == id);

    public IEnumerable<Transaction> All()
        => _records;

    public void Update(Transaction draft)
    {
    }
}