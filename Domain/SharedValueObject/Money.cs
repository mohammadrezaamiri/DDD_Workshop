using Domain.Exceptions;

namespace Domain.SharedValueObject;

public class Money : ValueObject
{
    public decimal Value { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new MoneyCanNotBeNegativeException();
        Value = amount;
    }

    public static Money operator -(Money left, Money right)
        => new(left.Value - right.Value);

    public static Money operator +(Money left, Money right)
        => new(left.Value + right.Value);

    public static implicit operator Money(decimal amount)
        => new(amount);

    public static bool operator <(Money left, Money right)
        => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
        => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
        => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
        => left.Value >= right.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}