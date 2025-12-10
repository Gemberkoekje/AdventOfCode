using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace AdventOfCode2025;

public static class Data
{
    public static char[][] Grid { get; set; }
}

public sealed class Day9 : ICanGiveASolution
{
    [Theory]
    [InlineData("7,1\r\n11,1\r\n11,7\r\n9,7\r\n9,5\r\n2,5\r\n2,3\r\n7,3", "50")]
    [FileData(typeof(Day9), "4759420470")]
    public void Day9_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData("7,1\r\n11,1\r\n11,7\r\n9,7\r\n9,5\r\n2,5\r\n2,3\r\n7,3", "24")]
    [InlineData("1,1\r\n3,1\r\n6,2\r\n8,2\r\n3,3\r\n6,3\r\n1,5\r\n8,5", "15")]
    [FileData(typeof(Day9), "4600181596")] // Too high
    public void Day9_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var set = input.ReadListAndCastToTuple2D<long>().ToList();
        long largest = 0;
        for (int a = 0; a < set.Count(); a++)
        {
            for (int b = a + 1; b < set.Count(); b++)
            {
                var item = set[a];
                var other = set[b];
                var area = (Math.Abs(item.X - other.X) + 1) * (Math.Abs(item.Y - other.Y) + 1);
                if (area > largest)
                {
                    largest = area;
                }
            }
        }
        return $"{largest}";
    }

    public string Solution2(string input)
    {
        var set = input.ReadListAndCastToTuple2D<long>().OrderBy(s => s.Y).ThenBy(s => s.X).ToList();
        long largest = 0;
        for (int a = 0; a < set.Count(); a++)
        {
            for (int b = a + 1; b < set.Count(); b++)
            {
                var item = set[a];
                var other = set[b];
                var pointsToCheck = set.Where(s => s != item && s != other && (s.X == item.X || s.Y == item.Y));
                bool blocked = false;
                foreach (var point in pointsToCheck)
                {
                    if (point.X == item.X && point.Y > Math.Min(item.Y, other.Y) && point.Y < Math.Max(item.Y, other.Y))
                    {
                        blocked = true;
                    }
                    if (point.Y == item.Y && point.X > Math.Min(item.X, other.X) && point.X < Math.Max(item.X, other.X))
                    {
                        blocked = true;
                    }
                }
                if (blocked)
                {
                    continue;
                }
                var pointsToCheck2 = set.Where(s => s != item && s != other && (s.X == other.X || s.Y == other.Y));
                bool blocked2 = false;
                foreach (var point in pointsToCheck2)
                {
                    if (point.X == other.X && point.Y > Math.Min(item.Y, other.Y) && point.Y < Math.Max(item.Y, other.Y))
                    {
                        blocked2 = true;
                    }
                    if (point.Y == other.Y && point.X > Math.Min(item.X, other.X) && point.X < Math.Max(item.X, other.X))
                    {
                        blocked2 = true;
                    }
                }
                if (blocked2)
                {
                    continue;
                }
                var area = (Math.Abs(item.X - other.X) + 1) * (Math.Abs(item.Y - other.Y) + 1);
                if (area > largest)
                {
                    largest = area;
                }
            }
        }
        return $"{largest}";
    }
}
