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
    [FileData(typeof(Day9), "1603439684")]
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
        var lines = new List<((long X, long Y), (long X, long Y))>();
        for (int a = 0; a < set.Count(); a++)
        {
            var others = set.Where(s => s != set[a] && (s.X == set[a].X || s.Y == set[a].Y)).ToList();
            for (int b = 0; b < others.Count(); b++)
            {
                if (!lines.Any(l => l == (others[b], set[a])))
                {
                    lines.Add((set[a], others[b]));
                }
            }
        }
        long largest = 0;
        for (int a = 0; a < set.Count(); a++)
        {
            for (int b = a + 1; b < set.Count(); b++)
            {
                var item = set[a];
                var other = set[b];
                bool docontinue = false;
                foreach(var line in lines)
                {
                    if (LineIntersectsWithRect(line.Item1, line.Item2, (Math.Min(item.X, other.X), Math.Min(item.Y, other.Y)), (Math.Max(item.X, other.X), Math.Max(item.Y, other.Y))))
                    {
                        docontinue = true;
                    }
                }
                if(docontinue)
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
    public bool LineIntersectsWithRect((long X, long Y) lineStart, (long X, long Y) lineEnd, (long X, long Y) rectTopLeft, (long X, long Y) rectBottomRight)
    {
        if (lineStart.X >= rectBottomRight.X && lineEnd.X >= rectBottomRight.X) return false;
        if (lineStart.X <= rectTopLeft.X && lineEnd.X <= rectTopLeft.X) return false;
        if (lineStart.Y >= rectBottomRight.Y && lineEnd.Y >= rectBottomRight.Y) return false;
        if (lineStart.Y <= rectTopLeft.Y && lineEnd.Y <= rectTopLeft.Y) return false;
        return true;
    }
}
