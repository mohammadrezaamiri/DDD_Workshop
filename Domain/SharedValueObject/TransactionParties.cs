using Domain.Account;

namespace Domain.SharedValueObject;

public class TransactionParties
{
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }

    public TransactionParties(
        AccountId creditAccountId,
        AccountId debitAccountId)
    {
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
    }
}