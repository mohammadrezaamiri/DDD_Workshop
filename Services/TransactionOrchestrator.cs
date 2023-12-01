﻿using Domain;
using Domain.Account;
using Domain.Exceptions;
using Domain.SharedValueObject;
using Domain.Transaction;

namespace Services;

public class TransactionOrchestrator
{
    readonly ITransactions _transactions;
    readonly ITransferService _transferService;
    public TransactionOrchestrator(
        ITransactions transactions, 
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
