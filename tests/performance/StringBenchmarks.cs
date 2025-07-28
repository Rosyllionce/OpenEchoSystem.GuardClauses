using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using OpenEchoSystem.GuardClauses;
using System.Text.RegularExpressions;

namespace OpenEchoSystem.GuardClauses.PerformanceTests;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class StringBenchmarks
{
    private const string ValidEmail = "test@example.com";
    private const string ValidUrl = "https://www.example.com";
    
    private const string EmailRegexPattern = @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$";
    private const string UrlRegexPattern = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    // Benchmarks for Email Validation
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Email")]
    public void GuardAgainstInvalidEmailPrecompiled()
    {
        Guard.Against.InvalidEmail(ValidEmail, nameof(ValidEmail));
    }

    [Benchmark]
    [BenchmarkCategory("Email")]
    public void GuardAgainstInvalidEmailNonPrecompiled()
    {
        if (!Regex.IsMatch(ValidEmail, EmailRegexPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
        {
            throw new ArgumentException("Invalid email format.", nameof(ValidEmail));
        }
    }

    // Benchmarks for URL Validation
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("URL")]
    public void GuardAgainstInvalidUrlPrecompiled()
    {
        Guard.Against.InvalidUrl(ValidUrl, nameof(ValidUrl));
    }

    [Benchmark]
    [BenchmarkCategory("URL")]
    public void GuardAgainstInvalidUrlNonPrecompiled()
    {
        if (!Regex.IsMatch(ValidUrl, UrlRegexPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
        {
            throw new ArgumentException("Invalid URL format.", nameof(ValidUrl));
        }
    }
}