using Domain.SharedValueObject;
using FluentAssertions;
using MessageBus.DomainEventsBus;
using Moq;
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
    private readonly EFWriteDataContext _writeDbContext;
    private readonly DraftTransferCommandHandler _draftSut;
    private readonly CommitTransferCommandHandler _commitSut;
    private readonly OpenAccountCommandHandler _accountCommandHandler;
    private readonly AccountQueries _accountQueries;
    private readonly TransactionQueries _transactionQueries;

    public TransactionOrchestratorSpecs()
    {
        var db = new EFInMemoryDatabase();
        var dispatcher = new Mock<IDomainEventDispatcher>().Object;
        _writeDbContext = db.CreateDataContext<EFWriteDataContext>();
        var transactions = new TransactionRepository(dispatcher, _writeDbContext);
        _draftSut = new DraftTransferCommandHandler(transactions);
        var accounts = new AccountRepository(dispatcher, _writeDbContext);
        var transferService = new TransferService(accounts);
        _commitSut = new CommitTransferCommandHandler(transactions, transferService);
        _accountCommandHandler = new OpenAccountCommandHandler(accounts);
        var readDataContext = db.CreateDataContext<EFReadDataContext>();
        _accountQueries = new AccountQueries(readDataContext);
        _transactionQueries = new TransactionQueries(readDataContext);
    }
    
    [Theory, AutoMoqData]
    public void Transfer_adds_the_balance_to_the_debit_account(
        DraftTransferCommand draftCommand)
    {
        TestSut(() => _accountCommandHandler.Handle(new OpenAccountCommand(
                draftCommand.CreditAccountId,
                (draftCommand.Amount + new Money(20000)).Value)));
        
        TestSut(() => _draftSut.Handle(draftCommand));

        TestSut(() => _commitSut.Handle(new CommitTransferCommand(draftCommand.TransactionId)));
        
        _accountQueries.GetBalanceForAccount(draftCommand.DebitAccountId).Should()
            .BeEquivalentTo(new { Balance = draftCommand.Amount });
    }


    [Theory, AutoMoqData]
    public void Transfer_subtracts_the_balance_from_the_credit_account(
        DraftTransferCommand draftCommand)
    {
        var creditAccount = Build.AnAccount
            .WithId(draftCommand.CreditAccountId)
            .WithBalance(draftCommand.Amount + new Money(25000)).Please();

        TestSut(() => _accountCommandHandler.Handle(
            new OpenAccountCommand(
                draftCommand.CreditAccountId, creditAccount.Balance.Value)));

        TestSut(() => _draftSut.Handle(draftCommand));

        TestSut(() => _commitSut.Handle(new CommitTransferCommand(draftCommand.TransactionId)));

        _accountQueries.GetBalanceForAccount(draftCommand.CreditAccountId).Should()
            .BeEquivalentTo(new { Balance = 25000 });
    }

    [Theory, AutoMoqData]
    public void Drafts_a_new_transaction(
        DraftTransferCommand command)
    {
        TestSut(() => _draftSut.Handle(command));

        _transactionQueries.AllDrafts().Should().Contain(new TransferDraftViewModel(
            command.CreditAccountId,
            command.DebitAccountId,
            command.Amount,
            command.TransactionDate
        ));
    }

    private void TestSut(Action action)
    {
        action.Invoke();
        _writeDbContext.SaveChanges();
    }
}