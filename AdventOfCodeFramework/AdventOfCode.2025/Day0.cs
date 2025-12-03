using AdventOfCode.Framework;
using AdventOfCodeFramework;

namespace AdventOfCode2025;

public sealed class Day0 : ICanGiveASolution
{
    [Theory]
    [InlineData("data", "answer")]
    [FileData(typeof(Day0), "answer")]
    public void Day0_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("data", "answer")]
    [FileData(typeof(Day0), "answer")]
    public void Day0_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
         return $"answer";
    }

    public string Solution2(string input)
    {
        return $"answer";
    }
}
