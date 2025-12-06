using AdventOfCode.Framework;
using AdventOfCodeFramework;

namespace AdventOfCode2025;

public sealed class Day5 : ICanGiveASolution
{
    [Theory]
    [InlineData("3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32", "3")]
    [FileData(typeof(Day5), "707")]
    public void Day5_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32", "14")]
    [InlineData("3-9\r\n5-7", "7")]
    [FileData(typeof(Day5), "361615643045059")]
    public void Day5_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var lists = input.Split("\r\n\r\n");
        var freshlist = lists[0].ReadList<long>("-");
        var ingredientlist = lists[1].ReadList<long>();
        freshlist = freshlist.OrderBy(v => v[0]).ToArray();
        ingredientlist = ingredientlist.OrderBy(v => v).ToArray();
        var total = 0;
        foreach (var value in ingredientlist)
        {
            if (freshlist.Any(v => v[0] <= value && v[1] >= value))
            {
                total++;
            }
        }

        return $"{total}";
    }

    public string Solution2(string input)
    {
        var lists = input.Split("\r\n\r\n");
        var freshlist = lists[0].ReadList<long>("-");
        freshlist = freshlist.OrderBy(v => v[0]).ToArray();
        var index = 0;
#if DEBUG
        foreach (var value in freshlist)
        {
            Console.WriteLine($"{value[0]} - {value[1]}");
        }
        Console.WriteLine();
#endif
        for (index = 0; index < freshlist.Length - 1; index++)
        {
            if (freshlist[index][1] >= freshlist[index + 1][0] - 1)
            {
                if (freshlist[index][1] > freshlist[index + 1][1])
                {
                    freshlist[index + 1][1] = freshlist[index][1];
                }
                freshlist[index][1] = freshlist[index + 1][0] - 1;
            }

        }
#if DEBUG
        foreach (var value in freshlist)
        {
            Console.WriteLine($"{value[0]} - {value[1]}");
        }
        Console.WriteLine();
#endif
        long total = 0;

        foreach (var value in freshlist)
        {
            total += (value[1] - value[0] + 1);
        }
        return $"{total}";
    }
}
