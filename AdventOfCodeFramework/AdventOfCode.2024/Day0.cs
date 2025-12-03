using AdventOfCodeFramework;

namespace AdventOfCode2024;

public sealed class Day0
{
    [Theory]
    [InlineData("data", "answer")]
    [FileData(typeof(Day0), "answer")]
    public void Day0_1(string input, string answer)
        => Solution0_1(input).ShouldBe(answer);

    [Theory]
    [InlineData("data", "answer")]
    [FileData(typeof(Day0), "answer")]
    public void Day0_2(string input, string answer)
        => Solution0_2(input).ShouldBe(answer);

    public static string Solution0_1(string input)
    {
         return $"answer";
    }

    public static string Solution0_2(string input)
    {
        return $"answer";
    }
}
