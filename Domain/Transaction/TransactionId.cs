using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Domain.Transaction;

public class TransactionId: ValueObject
{
    public TransactionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new TransactionIdIsNotValidException();
        
        Value = value;
    }

    public string Value { get; }
    
    public static implicit operator TransactionId(string id)
        => new(id);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}