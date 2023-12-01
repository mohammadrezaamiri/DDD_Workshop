using Domain.Transaction;
using Services;

namespace Queries;

public record TransferDraftViewModel(
    string CreditAccountId,
    string DebitAccountId,
    decimal Balance,
    DateTime Date);

public class TransactionQueries
{
    readonly ITransactions _transactions;
    
    public TransactionQueries(ITransactions transactions)
        => _transactions = transactions;

    public IEnumerable<TransferDraftViewModel> AllDrafts()
        => _transactions.All()
            .Where(t => t.Status == TransferStatus.Draft)
            .Select(t => new TransferDraftViewModel(
                t.Parties.CreditAccountId.Value,
                t.Parties.DebitAccountId.Value,
                t.Amount.Value,
                t.Date
            ));
}