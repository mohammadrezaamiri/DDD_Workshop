using Domain.SharedValueObject;
using Domain.Transaction;
using MessageBus;

namespace Services.TransactionStories.DraftTransfer;

public class DraftTransferCommandHandler 
    : ICommandHandler<DraftTransferCommand>
{
    private readonly ITransactions _transactions;

    public DraftTransferCommandHandler(ITransactions transactions)
        => _transactions = transactions;

    public void Handle(DraftTransferCommand command)
    {
        var parties = new TransactionParties(
            command.CreditAccountId, command.DebitAccountId);
        
        var request = new TransferRequest(parties, command.Amount);
        
        _transactions.Add(
            Transaction.Draft(
                    command.TransactionId, 
                    command.TransactionDate, 
                    request)
                .WithDraftDescription(command.Description));
    }
}