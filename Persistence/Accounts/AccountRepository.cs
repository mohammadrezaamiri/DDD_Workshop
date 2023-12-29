using Domain.Account;
using MessageBus.DomainEventsBus;
using Services.AccountStories;

namespace Persistence.Accounts;

public class AccountRepository: IAccounts
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly EFWriteDataContext _dbContext;

    public AccountRepository(
        IDomainEventDispatcher dispatcher,
        EFWriteDataContext dbContext)
    {
        _dispatcher = dispatcher;
        _dbContext = dbContext;
    }

    public Account? FindById(AccountId id)
        => _dbContext.Set<Account>().FirstOrDefault(a => a.Id == id);

    public void Update(Account account)
    {
        var record = FindById(account.Id);
    }

    public void Add(Account account)
    {
        _dbContext.Set<Account>().Add(account);
        _dispatcher.Dispatch(account.NewEvents);
        account.ClearEvents();
    }
}