using Domain.Account;

namespace Services.AccountStories;

public interface IAccounts
{
    Account? FindById(AccountId id);
    void Update(Account account);
    void Add(Account account);
    List<Account> All();
}