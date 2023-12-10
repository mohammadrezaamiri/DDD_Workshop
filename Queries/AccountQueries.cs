using Services.AccountStories;

namespace Queries;

public class AccountQueries
{
    readonly IAccounts _accounts;
    
    public AccountQueries(IAccounts accounts)
        => _accounts = accounts;
    
    public BalanceViewModel? GetBalanceForAccount(string accountId)
    {
        var theAccount= _accounts.FindById(accountId);
        return theAccount is null 
            ? null 
            : new BalanceViewModel(theAccount.Id.Value, theAccount.Balance.Value);
    }
}