using BenchmarkDotNet.Running;

namespace OpenEchoSystem.GuardClauses.PerformanceTests;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<StringBenchmarks>();
    }
}