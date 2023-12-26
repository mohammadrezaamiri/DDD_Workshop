using Domain.Transaction;

namespace Queries.Transactions;

public class TransactionQueries
{
    private readonly EFReadDataContext _dbContext;
    
    public TransactionQueries(EFReadDataContext dbContext)
        => _dbContext = dbContext;

    public IEnumerable<TransferDraftViewModel> AllDrafts()
        => _dbContext.Set<Transaction>()
            .Where(t => t.Status == TransferStatus.Draft)
            .Select(t => new TransferDraftViewModel(
                t.Request.Parties.CreditAccountId.Value,
                t.Request.Parties.DebitAccountId.Value,
                t.Request.Amount.Value,
                t.Date
            ));
}