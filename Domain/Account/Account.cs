using Domain.Account.Events;
using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Domain.Account;

public class Account : AggregateRoot
{
    public Account(AccountId id, Money initialBalance)
    {
        Id = id;
        Balance = initialBalance;
        newEvents.Enqueue(new AccountOpened(id.Value, initialBalance.Value));
    }
    public AccountId Id { get; }
    public Money Balance { get; private set; }

    public void Credit(Money amount)
    { 
        if (Balance <= amount)
            throw new NotEnoughChargeException();
        
        Balance -= amount;
    }

    public void Debit(Money amount)
        => Balance += amount;       
}