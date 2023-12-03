using Domain.Account;
using Domain.Exceptions;
using FluentAssertions;

namespace Domain.Spec;

public class AccountIdSpecs
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void id_should_not_be_null_or_white_space(string id)
        => new Action(() =>
                new AccountId(id))
            .Should().ThrowExactly<AccountIdIsNotValidException>();

    [Fact]
    public void equality_should_work_correctly()
    {
        var id = Guid.NewGuid().ToString();
        
        var left = new AccountId(id);
        var right = new AccountId(id);
        
        (left == right).Should().BeTrue();
        left.Equals(right).Should().BeTrue();
        left.GetHashCode().Should().Be(right.GetHashCode());
    }
}