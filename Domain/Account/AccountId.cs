using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Domain.Account;

public class AccountId: ValueObject
{
    public AccountId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new AccountIdIsNotValidException();
        
        Value = value;
    }

    public string Value { get; private set; }
    
    public static implicit operator AccountId(string id)
        => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}