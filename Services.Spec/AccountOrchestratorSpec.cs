using AutoFixture.Xunit2;
using FluentAssertions;
using Persistence.InMemory;
using Queries;
using TestTools.Doubles;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(
        string accountId,
        [Frozen(Matching.ImplementedInterfaces)] Accounts _,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        var balance = Build.ValidMoney();
        accountOrchestrator.OpenAccount(accountId, balance.Value);
        queries.GetBalanceForAccount(accountId)
            .Should().BeEquivalentTo(new { Balance = balance.Value });
    }
}