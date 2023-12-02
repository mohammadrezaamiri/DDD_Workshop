using Domain.Account;

namespace Services;

public interface IAccounts
{
    Account? FindById(AccountId id);
    void Update(Account account);
    void Add(Account account);
}