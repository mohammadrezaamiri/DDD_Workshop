using Domain.Account;
using Domain.SharedValueObject;

namespace Domain.Transaction;

public enum TransferStatus
{
    Commit,
    Draft,
}

public class Transaction
{
    public TransactionId Id { get; }
    public AccountId CreditAccountId { get; }
    public AccountId DebitAccountId { get; }
    public Money Amount { get; }
    public DateTime Date { get; }
    public string? Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(
        TransactionId id,
        DateTime date,
        AccountId creditAccountId,
        AccountId debitAccountId,
        Money amount)
    {
        Id = id;
        Date = date;
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }

    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        AccountId creditAccountId,
        AccountId debitAccountId,
        Money amount)
        => new (
            id,
            date,
            creditAccountId,
            debitAccountId,
            amount
        );

    public Transaction WithDraftDescription(string description)
    {
        Description = description;
        return this;
    }
    
    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(CreditAccountId, DebitAccountId, Amount);
        Status = TransferStatus.Commit;
    }
}