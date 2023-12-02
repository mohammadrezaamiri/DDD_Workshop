using AutoFixture.Xunit2;
using Domain.Exceptions;
using Domain.SharedValueObject;
using FluentAssertions;
using TestTools.Doubles;

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
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney - smallerMoney)
            .Value.Should().Be(five);
    }

    [Fact]
    public void Supports_addition()
    {
        var left = Build.ValidMoney();
        var right = Build.ValidMoney();

        (left + right)
            .Value.Should().Be(left.Value + right.Value);
    }

    [Theory, AutoData]
    public void subtraction_throw_exception_when_result_is_negative(uint five)
    {
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        var actual = () => smallerMoney - biggerMoney;

        actual.Should().ThrowExactly<MoneyCanNotBeNegativeException>();
    }

    [Theory, AutoData]
    public void Supports_less_than(uint five)
    {
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (smallerMoney < biggerMoney).Should().BeTrue();
    }  
    
    [Theory, AutoData]
    public void Supports_greater_than(uint five)
    {
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney > smallerMoney).Should().BeTrue();
    }

    [Fact]
    public void Supports_greater_than_or_equal_when_is_equal()
    {
        var left = Build.ValidMoney();
        var right = left;

        (left >= right).Should().BeTrue();
    }
    
    [Theory, AutoData]
    public void Supports_greater_than_or_equal_when_is_greater(uint five)
    {
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (biggerMoney >= smallerMoney).Should().BeTrue();
    }  
    
    [Fact]
    public void Supports_less_than_or_equal_when_is_equal()
    {
        var left = Build.ValidMoney();
        var right = left;

        (left <= right).Should().BeTrue();
    }
    
    [Theory, AutoData]
    public void Supports_less_than_or_equal_when_is_less(uint five)
    {
        var smallerMoney = Build.ValidMoney();
        var biggerMoney = new Money(smallerMoney.Value + five);

        (smallerMoney <= biggerMoney).Should().BeTrue();
    }

    [Fact]
    public void Equals_another_object_with_the_same_value()
    {
        var theSameAmount = Build.ValidMoney();
        var left = new Money(theSameAmount.Value);
        var right = new Money(theSameAmount.Value);
        
        left.Equals(right).Should().BeTrue();
    }
    
    [Fact]
    public void Doesnt_equal_different_object()
    {
        var theSameAmount = Build.ValidMoney();
        var left = new Money(theSameAmount.Value);
        var right = new MoneySpecs();
        
        left.Equals(right).Should().BeFalse();
    }
    
    [Fact]
    public void Equals_another_object_with_the_same_value_by_compare_equal_operator()
    {
        var theSameAmount = Build.ValidMoney();
        var left = new Money(theSameAmount.Value);
        var right = new Money(theSameAmount.Value);

        (left == right).Should().BeTrue();
    }
    
    
    [Fact]
    public void Not_equals_another_object_with_the_same_value_by_compare_not_equal_operator()
    {
        var theSameAmount = Build.ValidMoney();
        var left = new Money(theSameAmount.Value + 100);
        var right = new Money(theSameAmount.Value);

        (left != right).Should().BeTrue();
    }
}