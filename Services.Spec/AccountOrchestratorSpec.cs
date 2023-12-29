using FluentAssertions;
using MessageBus.DomainEventsBus;
using Moq;
using Persistence;
using Persistence.Accounts;
using Queries;
using Queries.Accounts;
using Services.AccountStories.OpenAccount;
using TestTools.Doubles;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    private readonly OpenAccountCommandHandler _sut;
    private readonly AccountQueries _queries;
    private readonly EFWriteDataContext _writeDbContext;

    public AccountOrchestratorSpec()
    {
        var db = new EFInMemoryDatabase();
        _writeDbContext = db.CreateDataContext<EFWriteDataContext>();
        _sut = GenerateSut(_writeDbContext);
        _queries = CreateQueries(db);
    }

    [Theory, AutoMoqData]
    public void Opens_a_new_account(string accountId)
    {
        var balance = Build.ValidMoney();
        TestSut(() => _sut.Handle(new OpenAccountCommand(accountId, balance.Value)));
        
        _queries.GetBalanceForAccount(accountId)
            .Should().BeEquivalentTo(new { Balance = balance.Value });
    }
    
    private AccountQueries CreateQueries(EFInMemoryDatabase db)
    {
        var readDbContext = db.CreateDataContext<EFReadDataContext>();
        return new AccountQueries(readDbContext);
    }

    private OpenAccountCommandHandler GenerateSut(EFWriteDataContext db)
    {
        var dispatcher = new Mock<IDomainEventDispatcher>().Object;
        var accountRepository = new AccountRepository(dispatcher, db);
        return new OpenAccountCommandHandler(accountRepository);
    }
    
    private void TestSut(Action action)
    {
        action.Invoke();
        _writeDbContext.SaveChanges();
    }
}