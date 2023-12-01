using AutoFixture;

namespace DomainTests;

public static class Build
{
    public static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        return customization == null ? t : customization(t);
    }
}