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
    public TransactionParties Parties { get; set; }
    public Money Amount { get; }
    public DateTime Date { get; }
    public string? Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(
        TransactionId id,
        DateTime date,
        TransactionParties parties,
        Money amount)
    {
        Id = id;
        Date = date;
        Parties = parties; 
        Amount = amount;
    }

    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        TransactionParties parties,
        Money amount)
        => new (id, date, parties, amount);

    public Transaction WithDraftDescription(string description)
    {
        Description = description;
        return this;
    }
    
    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(Parties, Amount);
        Status = TransferStatus.Commit;
    }
}