using Domain;
using Domain.Exceptions;
using Domain.Transaction;
using MessageBus;

namespace Services.TransactionStories.CommitTransfer;

public class CommitTransferCommandHandler
    : ICommandHandler<CommitTransferCommand>
{
    private readonly ITransactions _transactions;
    private readonly ITransferService _transferService;
    
    public CommitTransferCommandHandler(
        ITransactions transactions, 
        ITransferService transferService)
    {
        _transactions = transactions;
        _transferService = transferService;
    }

    public void Handle(CommitTransferCommand command)
        => CommitTransfer(command.TransactionId);
    
    private void CommitTransfer(
        TransactionId transactionId)
    {
        var draft = _transactions.FindById(transactionId);

        if(draft is null) throw new TransactionNotFoundException();
        
        draft.Commit(_transferService);

        _transactions.Update(draft);
    }
}