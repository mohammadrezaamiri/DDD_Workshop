using Services.Domain.Account;

namespace Services;

public class Accounts
{
    private readonly List<Account> _records = new();
    public Account? FindById(AccountId id)
        => _records.FirstOrDefault(a => a.Id == id);

    public void Update(Account account)
    {
        var record = FindById(account.Id);
    }

    public void Add(Account account) => _records.Add(account);
    
    public void Clear() => _records.Clear();
}