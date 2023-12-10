using AutoFixture.Xunit2;
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
    
    [Fact]
    public void equality_should_work_correctly()
    {
        var id = Guid.NewGuid().ToString();
        
        var left = new TransactionId(id);
        var right = new TransactionId(id);
        
        (left == right).Should().BeTrue();
        left.Equals(right).Should().BeTrue();
        left.GetHashCode().Should().Be(right.GetHashCode());
    }
    
    [Theory, AutoData]
    public void not_equality_should_work_correctly(
        TransactionId left, TransactionId right)
        => (left != right).Should().BeTrue();
}