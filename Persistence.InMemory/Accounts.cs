using Domain.Account;
using MessageBus.DomainEventsBus;
using Services.AccountStories;

namespace Persistence.InMemory;

public class Accounts: IAccounts
{
    private readonly List<Account> _records = new();
    private readonly IDomainEventDispatcher _dispatcher;

    public Accounts(IDomainEventDispatcher dispatcher)
        => _dispatcher = dispatcher;

    public Account? FindById(AccountId id)
        => _records.FirstOrDefault(a => a.Id == id);

    public void Update(Account account)
    {
        var record = FindById(account.Id);
    }

    public void Add(Account account)
    {
        _records.Add(account);
        _dispatcher.Dispatch(account.NewEvents);
        account.ClearEvents();
    }
    public List<Account> All()
    {
        return _records.ToList();
    }

    public void Clear() => _records.Clear();
}