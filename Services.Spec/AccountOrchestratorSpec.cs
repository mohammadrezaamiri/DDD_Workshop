using AutoFixture.Xunit2;
using FluentAssertions;
using Queries;
using Services.Domain.Account;
using Services.Domain.SharedValueObject;
using TestTools.Doubles;

namespace Services.Spec;

public class AccountOrchestratorSpec
{
    [Theory, AutoMoqData]
    public void Opens_a_new_account(AccountId accountId,
        [Frozen] Accounts _,
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