using Domain.Account;

namespace Queries.Accounts;

public class AccountQueries
{
    private readonly EFReadDataContext _dbContext;
    
    public AccountQueries(EFReadDataContext dbContext)
        => _dbContext = dbContext;
    
    public BalanceViewModel? GetBalanceForAccount(string accountId)
        => _dbContext.Set<Account>()
            .Where(_ => _.Id.Value == accountId)
            .Select(_ => new BalanceViewModel(_.Id.Value, _.Balance.Value))
            .FirstOrDefault();

    public List<BalanceViewModel> GetAll()
        => _dbContext.Set<Account>()
            .Select(_ => new BalanceViewModel(_.Id.Value, _.Balance.Value))
            .ToList();
}