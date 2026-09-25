using Xunit;
using Greeting;

namespace Greeting.Tests;

public class GreetingTests
{
    [Fact]
    public void TestGreet()
    {
        var result = Greeter.Greet(".NET");
        Assert.Equal("Hello, .NET!", result);
    }

    [Fact]
    public void TestSumRange()
    {
        var result = Greeter.SumRange(1, 10);
        Assert.Equal(55, result);
    }
}
