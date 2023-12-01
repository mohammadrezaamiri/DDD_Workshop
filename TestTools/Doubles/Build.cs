using AutoFixture;

namespace TestTools.Doubles;

public static class Build
{
    public static AnAccount AnAccount => new AnAccount();
    
    public static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        return customization == null ? t : customization(t);
    }
}