using Services.Domain.Exceptions;

namespace Services.Domain.Account;

public class AccountId
{
    public AccountId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new AccountIdIsNotValidException();
        
        Value = id;
    }

    public string Value { get; }
    
    public static implicit operator AccountId(string id)
        => new(id);
}