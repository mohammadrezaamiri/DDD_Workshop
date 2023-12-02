using Domain.SharedValueObject;
using FluentAssertions;
using TestTools.Doubles;

namespace DomainTests;

public class MoneyEqualitySpecs
{
    
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
    
    [Fact]
    public void Equals_the_same_reference_object()
    {
        var left = Build.ValidMoney();
        var right = left;
        left.Equals(right).Should().BeTrue();
        (left == right).Should().BeTrue();
    }
}