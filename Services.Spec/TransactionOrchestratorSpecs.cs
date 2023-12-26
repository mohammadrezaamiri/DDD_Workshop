using AutoFixture.Xunit2;
using Domain.Account;
using Domain.SharedValueObject;
using FluentAssertions;
using Persistence;
using Persistence.Accounts;
using Persistence.Transactions;
using Queries;
using Queries.Accounts;
using Queries.Transactions;
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
        [Frozen(Matching.ImplementedInterfaces)] AccountRepository __,
        [Frozen(Matching.ImplementedInterfaces)] TransactionRepository ___,
        [Frozen(Matching.ImplementedInterfaces)] TransferService _,
        DraftTransferCommand draftCommand,
        DraftTransferCommandHandler draftSut,
        CommitTransferCommandHandler commitSut,
        OpenAccountCommandHandler accountOrchestrator,
        AccountQueries queries)
    {
        accountOrchestrator.Handle(new OpenAccountCommand(
                draftCommand.CreditAccountId,
                (draftCommand.Amount + new Money(20000)).Value));
        
        draftSut.Handle(draftCommand);

        commitSut.Handle(new CommitTransferCommand(draftCommand.TransactionId));

        queries.GetBalanceForAccount(draftCommand.DebitAccountId).Should()
            .BeEquivalentTo(new { Balance = draftCommand.Amount });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        [Frozen(Matching.ImplementedInterfaces)] AccountRepository __,
        [Frozen(Matching.ImplementedInterfaces)] TransactionRepository ___,
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
        [Frozen(Matching.ImplementedInterfaces)] TransactionRepository _,
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