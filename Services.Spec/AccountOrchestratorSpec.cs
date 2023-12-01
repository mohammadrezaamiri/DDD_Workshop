using AutoFixture.Xunit2;
using Domain.Account;
using Domain.SharedValueObject;
using FluentAssertions;
using Persistence;
using Queries;
using TestTools.Doubles;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(AccountId accountId,
        [Frozen(Matching.ImplementedInterfaces)] Accounts _,
        AccountQueries queries,
        AccountOrchestrator accountOrchestrator
    )
    {
        var balance = AValidMoney();
        accountOrchestrator.OpenAccount(accountId, balance);
        queries.GetBalanceForAccount(accountId)
            .Should().BeEquivalentTo(new { Balance = balance.Value });
    }

    private static Money AValidMoney()
        => Build.A<Money>(with => 
            new Money(Math.Abs(with.Value)));
}