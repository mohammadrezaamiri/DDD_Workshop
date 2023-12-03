using AutoFixture.Xunit2;
using Domain.Account;
using Domain.Exceptions;
using Domain.SharedValueObject;
using FluentAssertions;

namespace Domain.Spec;

public class TransactionPartiesSpecs
{
    [Fact]
    public void credit_account_id_should_not_be_same_with_debit_account()
    {
        var id = Guid.NewGuid().ToString();

        var creditAccountId = new AccountId(id);
        var debitAccountId = new AccountId(id);

        new Action(() => new TransactionParties(creditAccountId, debitAccountId))
            .Should().ThrowExactly<PartiesCanNotBeSameException>();
    }


    [Theory, AutoData]
    public void equality_should_work_correctly(
        AccountId creditAccountId, AccountId debitAccountId)
    {
        var left = new TransactionParties(creditAccountId, debitAccountId);
        var right = new TransactionParties(creditAccountId, debitAccountId);

        (left == right).Should().BeTrue();
        left.Equals(right).Should().BeTrue();
        left.GetHashCode().Should().Be(right.GetHashCode());
    }

    [Theory, AutoData]
    public void not_equality_should_work_correctly(
        TransactionParties left, TransactionParties right)
        => (left != right).Should().BeTrue();
}