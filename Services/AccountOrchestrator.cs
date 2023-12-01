using Services.Domain.Account;
using Services.Domain.SharedValueObject;

namespace Services;

public class AccountOrchestrator
{
    private readonly Accounts _accounts;
    public AccountOrchestrator(Accounts accounts)
        => _accounts = accounts;

    public void OpenAccount(
        AccountId accountId, Money initialBalance)
        => _accounts.Add(new Account(accountId, initialBalance));
}