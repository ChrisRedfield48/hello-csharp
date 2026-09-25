namespace Greeting;

public static class Greeter
{
    public static string Greet(string name) => $"Hello, {name}!";

    public static int SumRange(int from, int to)
    {
        int sum = 0;
        for (int i = from; i <= to; i++)
        {
            sum += i;
        }
        return sum;
    }
}
