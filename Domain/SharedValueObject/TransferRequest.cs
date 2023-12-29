namespace Domain.SharedValueObject;

public class TransferRequest: ValueObject
{
    private TransferRequest() { }
    
    public TransferRequest(TransactionParties parties, Money amount)
    {
        Parties = parties;
        Amount = amount;
    }
    
    public TransactionParties Parties { get; private set; }
    public Money Amount { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Parties;
    }
}