using AdventOfCodeFramework;

namespace AdventOfCode2024;

public sealed class Day1
{
    [Theory]
    [InlineData("3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "11")]
    [FileData(typeof(Day1), "765748")]
    public void Day1_1(string input, string answer)
        => Solution1_1(input).ShouldBe(answer);

    [Theory]
    [InlineData("3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "31")]
    [FileData(typeof(Day1), "27732508")]
    public void Day1_2(string input, string answer)
        => Solution1_2(input).ShouldBe(answer);

    public static string Solution1_1(string input)
    {
        var values = input.ReadVerticalList<int>("   ");
        var list1 = values[0];
        var list2 = values[1];
        var total = 0;
        var orderedlist1 = list1.Order().ToArray();
        var orderedlist2 = list2.Order().ToArray();
        for (int i = 0; i < orderedlist1.Length; i++)
        {
            total += Math.Abs(orderedlist1[i] - orderedlist2[i]);
        }
        return $"{total}";
    }

    public static string Solution1_2(string input)
    {
        var values = input.ReadVerticalList<int>("   ");
        var list1 = values[0];
        var list2 = values[1];
        var total = 0;

        foreach (var value in list1)
        {
            total += list2.Count(v => v == value) * value;
        }
        return $"{total}";
    }
}
