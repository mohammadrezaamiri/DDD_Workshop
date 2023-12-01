using Services.Domain.SharedValueObject;

namespace Services.Domain.Transaction;

public enum TransferStatus
{
    Commit,
    Draft,
}

public class Transaction
{


    public string CreditAccountId { get; }
    public string DebitAccountId { get; }
    public Money Amount { get; }

    public string Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public TransferStatus Status { get; private set; } = TransferStatus.Draft;

    protected Transaction(string id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        Money amount)
    {
        Id = id;
        Date = date;
        Description = description;
        CreditAccountId = creditAccountId;
        DebitAccountId = debitAccountId;
        Amount = amount;
    }

    public static Transaction Draft(
        string id,
        DateTime date,
        string description,
        string creditAccountId,
        string debitAccountId,
        Money amount)
        => new Transaction(
            id,
            date,
            description,
            creditAccountId,
            debitAccountId,
            amount
        );

    public void Commit(ITransferService transferService)
    {
        transferService.Transfer(CreditAccountId, DebitAccountId, Amount);
        Status = TransferStatus.Commit;
    }
}