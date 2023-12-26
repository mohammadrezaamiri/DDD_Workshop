using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Domain.Account;

public class Account : AggregateRoot
{
    public Account(AccountId id, Money balance)
    {
        Id = id;
        Balance = balance;
    }
    public AccountId Id { get; private set; }
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