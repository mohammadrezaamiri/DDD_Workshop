using AutoFixture.Xunit2;
using Domain.SharedValueObject;
using Domain.Transaction;
using FluentAssertions;
using Persistence;
using Queries;
using TestTools.Doubles;

namespace Services.Spec;

public class TransactionOrchestratorSpecs
{
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        TransactionParties parties,
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
            parties.CreditAccountId, amount + new Money(20000));

        sut.DraftTransfer(transactionId,
            parties,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(parties.DebitAccountId).Should()
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
        TransactionParties parties
        )
    {
        var creditAccount = Build.AnAccount
            .WithId(parties.CreditAccountId)
            .WithBalance(amount + new Money(25000)).Please();

        accountService.OpenAccount(creditAccount.Id, creditAccount.Balance.Value);

        sut.DraftTransfer(transactionId,
            parties,
            amount, now, description);

        sut.CommitTransfer(transactionId);

        queries.GetBalanceForAccount(creditAccount.Id).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen(Matching.ImplementedInterfaces)] Transactions _,
        TransactionOrchestrator sut,
        TransactionQueries queries,
        DateTime now,
        string description,
        TransactionParties parties,
        Money amount
    )
    {
        sut.DraftTransfer(
            "transaction Id", 
            parties,
            amount, 
            now, 
            description);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            parties.CreditAccountId.Value,
            parties.DebitAccountId.Value,
            amount.Value,
            now
        ));
    }
}