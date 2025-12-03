using AdventOfCode.Framework;
using AdventOfCodeFramework;

namespace AdventOfCode2025;

public sealed class Day1 : ICanGiveASolution
{
    [Theory]
    [InlineData("L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82", "3")]
    [FileData(typeof(Day1), "1066")]
    public void Day1_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82", "6")]
    [InlineData("L50\r\nL199", "2")]
    [InlineData("L50\r\nL200", "3")]
    [FileData(typeof(Day1), "6223")]
    public void Day1_2(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var values = input.ReadList();
        var start = 50;
        var amount = 0;
        foreach (var value in values)
        {
            var direction = value[0];
            var distance = int.Parse(value[1..]);
            if (direction == 'L')
            {
                start -= distance;
            }
            else if (direction == 'R')
            {
                start += distance;
            }
            while (start < 0)
            {
                start = 100 + start;
            }
            while (start > 99)
            {
                start = start - 100;
            }
            if (start == 0)
            {
                amount++;
            }
            //Console.WriteLine($"Position: {start}");
        }
        return $"{amount}";
    }

    public string Solution2(string input)
    {
        var values = input.ReadList();
        var start = 50;
        var amount = 0;
        foreach (var value in values)
        {
            var direction = value[0];
            var distance = int.Parse(value[1..]);
            bool alreadycounted = false;
            if (start == 0)
            {
                alreadycounted = true;
            }
            if (direction == 'L')
            {
                start -= distance;
            }
            else if (direction == 'R')
            {
                start += distance;
            }
            while (start < 0)
            {
                start = 100 + start;
                if (!alreadycounted)
                {
                    //Console.WriteLine($"Went through 0");
                    amount++;
                }
                if (alreadycounted)
                {
                    alreadycounted = false;
                }
            }
            while (start > 99)
            {
                start = start - 100;
                if (start != 0)
                {
                    //Console.WriteLine($"Went through 0");
                    amount++;
                }
            }
            if (start == 0)
            {
                amount++;
                //Console.WriteLine($"Finished on 0");
            }
            //Console.WriteLine($"Position: {start}");
        }
        return $"{amount}";
    }
}
