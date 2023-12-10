using AutoFixture.Xunit2;
using Domain.SharedValueObject;
using FluentAssertions;
using Persistence.InMemory;
using Queries;
using Services.AccountStories.OpenAccount;
using Services.TransactionStories;
using Services.TransactionStories.CommitTransfer;
using Services.TransactionStories.DraftTransfer;
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
        DraftTransferCommand draftCommand,
        DraftTransferCommandHandler draftSut,
        CommitTransferCommandHandler commitSut,
        OpenAccountCommandHandler accountOrchestrator,
        AccountQueries queries)
    {
        accountOrchestrator.Handle(new OpenAccountCommand(
                creditAccountId,
                (draftCommand.Amount + new Money(20000)).Value));

        draftSut.Handle(draftCommand);

        commitSut.Handle(new CommitTransferCommand(draftCommand.TransactionId));

        queries.GetBalanceForAccount(debitAccountId).Should()
            .BeEquivalentTo(new { Balance = draftCommand.Amount });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen(Matching.ImplementedInterfaces)] Accounts __,
        [Frozen(Matching.ImplementedInterfaces)] Transactions ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        DraftTransferCommandHandler draftSut,
        CommitTransferCommandHandler commitSut,
        OpenAccountCommandHandler accountService,
        DraftTransferCommand draftCommand,
        AccountQueries queries)
    {
        var creditAccount = Build.AnAccount
            .WithId(draftCommand.CreditAccountId)
            .WithBalance(draftCommand.Amount + new Money(25000)).Please();

        accountService.Handle(
            new OpenAccountCommand(
                draftCommand.CreditAccountId, creditAccount.Balance.Value));

        draftSut.Handle(draftCommand);

        commitSut.Handle(new CommitTransferCommand(draftCommand.TransactionId));

        queries.GetBalanceForAccount(draftCommand.CreditAccountId).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        [Frozen(Matching.ImplementedInterfaces)] Transactions _,
        DraftTransferCommandHandler sut,
        TransactionQueries queries,
        DraftTransferCommand command
    )
    {
        sut.Handle(command);

        queries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            command.CreditAccountId,
            command.DebitAccountId,
            command.Amount,
            command.TransactionDate
        ));
    }
}