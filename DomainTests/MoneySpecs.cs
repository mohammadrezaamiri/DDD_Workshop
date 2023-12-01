using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Services.Domain.Exceptions;
using Services.Domain.SharedValueObject;

namespace DomainTests;

public class MoneySpecs
{
    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)        
        => new Action(() =>
            new Money(-Math.Abs(amount))
        ).Should().ThrowExactly<MoneyCanNotBeNegativeException>();

    [Theory, AutoData]
    public void Supports_subtraction(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney - smallerMoney)
            .Value.Should().Be(five);
    }

    [Fact]
    public void Supports_addition()
    {
        var left = aValidMoney();
        var right = aValidMoney();

        (left + right)
            .Value.Should().Be(left.Value + right.Value);
    }

    [Theory, AutoData]
    public void subtraction_throw_exception_when_result_is_negative(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        var actual = () => smallerMoney - biggerMoney;

        actual.Should().ThrowExactly<MoneyCanNotBeNegativeException>();
    }

    [Theory, AutoData]
    public void Supports_less_than(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (smallerMoney < biggerMoney).Should().BeTrue();
    }  
    
    [Theory, AutoData]
    public void Supports_greater_than(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney > smallerMoney).Should().BeTrue();
    }

    [Fact]
    public void Supports_greater_than_or_equal_when_is_equal()
    {
        var left = aValidMoney();
        var right = left;

        (left >= right).Should().BeTrue();
    }
    
    [Theory, AutoData]
    public void Supports_greater_than_or_equal_when_is_greater(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney >= smallerMoney).Should().BeTrue();
    }  
    
    [Fact]
    public void Supports_less_than_or_equal_when_is_equal()
    {
        var left = aValidMoney();
        var right = left;

        (left <= right).Should().BeTrue();
    }
    
    [Theory, AutoData]
    public void Supports_less_than_or_equal_when_is_less(uint five)
    {
        var smallerMoney = aValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (smallerMoney <= biggerMoney).Should().BeTrue();
    }

    Money aValidMoney() =>
        Build.A<Money>(with => new Money(Math.Abs(with.Value)));
}