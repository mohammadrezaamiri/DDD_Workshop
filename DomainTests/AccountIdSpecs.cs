using Domain.Account;
using Domain.Exceptions;
using FluentAssertions;

namespace DomainTests;

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
}