using Services;
using Domain.Account;

namespace Queries;

public class AccountQueries
{
    readonly Accounts _accounts;
    
    public AccountQueries(Accounts accounts)
        => _accounts = accounts;
    
    public BalanceViewModel? GetBalanceForAccount(AccountId accountId)
    {
        var theAccount= _accounts.FindById(accountId);
        return theAccount is null 
            ? null 
            : new BalanceViewModel(theAccount.Id.Value, theAccount.Balance.Value);
    }
}