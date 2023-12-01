using Services.Domain;
using Services.Domain.Account;
using Services.Domain.Exceptions;
using Services.Domain.SharedValueObject;
using Services.Domain.Transaction;

namespace Services;

public class TransactionOrchestrator
{
    readonly Transactions _transactions;
    readonly ITransferService _transferService;
    public TransactionOrchestrator(
        Transactions transactions, 
        ITransferService transferService)
    {
        _transactions = transactions;
        _transferService = transferService;
    }

    public void DraftTransfer(
        TransactionId transactionId, 
        AccountId creditAccountId, 
        AccountId debitAccountId, 
        Money amount, 
        DateTime transactionDate, 
        string description)
    {
        _transactions.Add(
            Transaction.Draft(
                transactionId, 
                transactionDate, 
                creditAccountId, 
                debitAccountId, 
                amount)
                .WithDraftDescription(description));
        
    }

    public void CommitTransfer(
        TransactionId transactionId)
    {
        var draft = _transactions.FindById(transactionId);

        if(draft is null) throw new TransactionNotFoundException();
        
        draft.Commit(_transferService);

        _transactions.Update(draft);
    }
}
