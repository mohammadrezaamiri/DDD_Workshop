using Services;
using Domain.Account;

namespace Queries;

public class AccountQueries
{
    readonly IAccounts _accounts;
    
    public AccountQueries(IAccounts accounts)
        => _accounts = accounts;
    
    public BalanceViewModel? GetBalanceForAccount(AccountId accountId)
    {
        var theAccount= _accounts.FindById(accountId);
        return theAccount is null 
            ? null 
            : new BalanceViewModel(theAccount.Id.Value, theAccount.Balance.Value);
    }
}