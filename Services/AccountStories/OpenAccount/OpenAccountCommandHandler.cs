using Domain.Account;
using MessageBus;

namespace Services.AccountStories.OpenAccount;

public class OpenAccountCommandHandler 
    : IMessageHandler<OpenAccountCommand>
{
    private readonly IAccounts _accounts;

    public OpenAccountCommandHandler(IAccounts accounts)
        => _accounts = accounts;

    public void Handle(OpenAccountCommand command)
        => OpenAccount(command.AccountId, command.InitialBalance);

    private void OpenAccount(
        string accountId, decimal initialBalance)
        => _accounts.Add(
            new Account(accountId, initialBalance));
}