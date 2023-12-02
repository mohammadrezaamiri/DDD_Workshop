using Domain.Exceptions;

namespace Domain.SharedValueObject;

public class Money : IEquatable<Money>
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

    public static bool operator ==(Money? left, Money? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (ReferenceEquals(left, null))
            return false;
        return !ReferenceEquals(right, null) && left.Equals(right);
    }

    public static bool operator !=(Money left, Money right)
        => !(left == right);

    public bool Equals(Money? other)
    {
        if (ReferenceEquals(other, null))
            return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money money) return false;
        return money.Value == Value;
    }

    public override int GetHashCode()
        => Value.GetHashCode();
}