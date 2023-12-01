using Services;
using Services.Domain.Transaction;

namespace Queries;

public record TransferDraftViewModel(
    string CreditAccountId,
    string DebitAccountId,
    decimal Balance,
    DateTime Date);

public class TransactionQueries
{
    readonly Transactions _transactions;
    
    public TransactionQueries(Transactions transactions)
        => _transactions = transactions;

    public IEnumerable<TransferDraftViewModel> AllDrafts()
        => _transactions.All()
            .Where(t => t.Status == TransferStatus.Draft)
            .Select(t => new TransferDraftViewModel(
                t.CreditAccountId.Value,
                t.DebitAccountId.Value,
                t.Amount.Value,
                t.Date
            ));
}