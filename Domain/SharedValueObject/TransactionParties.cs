using Domain.Account;
using Domain.Exceptions;

namespace Domain.SharedValueObject;

public class TransactionParties: ValueObject
{
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }

    public TransactionParties(
        AccountId creditAccountId,
        AccountId debitAccountId)
    {
        if (creditAccountId == debitAccountId)
            throw new PartiesCanNotBeSameException();
        
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreditAccountId;
        yield return DebitAccountId;
    }
}