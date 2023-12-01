using Domain.Exceptions;

namespace Domain.Transaction;

public class TransactionId
{
    public TransactionId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new TransactionIdIsNotValidException();
        
        Id = id;
    }

    public string Id { get; }
    
    public static implicit operator TransactionId(string id)
        => new(id);
}