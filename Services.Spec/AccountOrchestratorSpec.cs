using AutoFixture.Xunit2;
using FluentAssertions;
using Persistence;
using Persistence.Accounts;
using Queries;
using Queries.Accounts;
using Services.AccountStories.OpenAccount;
using TestTools.Doubles;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(
        string accountId,
        [Frozen(Matching.ImplementedInterfaces)] AccountRepository _,
        AccountQueries queries,
        OpenAccountCommandHandler accountOrchestrator
    )
    {
        var balance = Build.ValidMoney();
        accountOrchestrator.Handle(new OpenAccountCommand(accountId, balance.Value));
        queries.GetBalanceForAccount(accountId)
            .Should().BeEquivalentTo(new { Balance = balance.Value });
    }
}