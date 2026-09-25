using System.Runtime.InteropServices;
using Greeting;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("hello-csharp version 1.0.0");
        Console.WriteLine("Hello from C#/.NET! 🟣⚡");
        Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
        Console.WriteLine($"Arch: {RuntimeInformation.OSArchitecture}");
        Console.WriteLine(Greeter.Greet("GitHub"));
        Console.WriteLine($"Sum 1..10 = {Greeter.SumRange(1, 10)}");

        if (args.Length > 0)
        {
            Console.WriteLine("Аргументы:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  {i + 1}: {args[i]}");
            }
        }
    }
}
