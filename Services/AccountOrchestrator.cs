using Domain.Account;
using Domain.SharedValueObject;

namespace Services;

public class AccountOrchestrator
{
    private readonly IAccounts _accounts;

    public AccountOrchestrator(IAccounts accounts)
        => _accounts = accounts;

    public void OpenAccount(
        string accountId, decimal initialBalance)
        => _accounts.Add(
            new Account(accountId,
                initialBalance));
}