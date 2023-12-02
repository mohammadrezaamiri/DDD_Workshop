namespace Domain.SharedValueObject;

public class TransferRequest: ValueObject
{
    public TransferRequest(TransactionParties parties, Money amount)
    {
        Parties = parties;
        Amount = amount;
    }
    public TransactionParties Parties { get; }
    public Money Amount { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Parties.CreditAccountId;
        yield return Parties.DebitAccountId;
    }
}