using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Domain.Account;

public class AccountId: ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}