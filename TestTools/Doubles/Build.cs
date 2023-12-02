using AutoFixture;
using Domain.SharedValueObject;

namespace TestTools.Doubles;

public static class Build
{
    public static AnAccount AnAccount => new AnAccount();
    
    public static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        return customization == null ? t : customization(t);
    }
    
    public static Money ValidMoney() =>
        A<Money>(with => new Money(Math.Abs(with.Value)));
}