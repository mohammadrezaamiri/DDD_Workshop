using Domain.Exceptions;
using Domain.Transaction;
using FluentAssertions;

namespace Domain.Spec;

public class TransactionIdSpecs
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void id_should_not_be_null_or_white_space(string id)
        => new Action(() =>
                new TransactionId(id))
            .Should().ThrowExactly<TransactionIdIsNotValidException>();
}