using AutoFixture.Xunit2;
using Domain.SharedValueObject;
using Domain.Transaction;
using FluentAssertions;
using Persistence;
using Persistence.InMemory;
using Queries;
using TestTools.Doubles;

namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        string creditAccountId,
        string debitAccountId,
        [Frozen(Matching.ImplementedInterfaces)] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] Transactions ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountOrchestrator,
        AccountQueries queries,
        TransactionId transactionId,
        Money amount,
        DateTime now,
        string description
    )
    {
        accountOrchestrator.OpenAccount(
            creditAccountId,
            (amount + new Money(20000)).Value);

        sut.DraftTransfer(transactionId.Value,
            creditAccountId,debitAccountId,
            amount.Value, now, description);

        sut.CommitTransfer(transactionId.Value);

        queries.GetBalanceForAccount(debitAccountId).Should()
            .BeEquivalentTo(new { Balance = amount.Value });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen(Matching.ImplementedInterfaces)] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] Transactions ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        TransactionOrchestrator sut,
        AccountOrchestrator accountService,
        AccountQueries queries,
        TransactionId transactionId,
        Money amount,
        DateTime now,
        string description,
        string creditAccountId,
        string debitAccountId
        )
    {
        var creditAccount = Build.AnAccount
            .WithId(creditAccountId)
            .WithBalance(amount + new Money(25000)).Please();

        accountService.OpenAccount(creditAccountId, creditAccount.Balance.Value);

        sut.DraftTransfer(transactionId,
            creditAccountId,debitAccountId,
            amount.Value, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccountId).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen(Matching.ImplementedInterfaces)] Transactions _,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        DateTime now,
        string description,
        Money amount,
        string creditAccountId,
        string debitAccountId
    )
    {
        sut.DraftTransfer(
            "transaction Id", 
            creditAccountId,
            debitAccountId,
            amount.Value, 
            now, 
            description);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            creditAccountId,
            debitAccountId,
            amount.Value,
            now
        ));
    }
}