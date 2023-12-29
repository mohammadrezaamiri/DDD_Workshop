using Domain.SharedValueObject;

namespace Domain.Transaction;

public enum TransferStatus
{
    Commit,
    Draft
}

public class Transaction : AggregateRoot
{
    private Transaction()
    {
        
    }
    public TransactionId Id { get; private set; }
    public TransferRequest Request { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(
        TransactionId id,
        DateTime date,
        TransferRequest request)
    {
        Id = id;
        Date = date;
        Request = request;
    }

    public static Transaction Draft(
        TransactionId id,
        DateTime date,
        TransferRequest request)
        => new (id, date, request);

    public Transaction WithDraftDescription(string description)
    {
        Description = description;
        return this;
    }
    
    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(Request);
        Status = TransferStatus.Commit;
    }
}