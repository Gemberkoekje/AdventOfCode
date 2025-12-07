using AdventOfCode.Framework;
using AdventOfCodeFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2025;

public sealed class Day7 : ICanGiveASolution
{
    [Theory]
    [InlineData(".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............", "21")]
    [FileData(typeof(Day7), "1543")]
    public void Day7_1(string input, string answer)
        => Solution1(input).ShouldBe(answer);

    [Theory]
    [InlineData(".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............", "40")]
    [FileData(typeof(Day7), "3223365367809")]
    public void Day7_2(string input, string answer)
        => Solution2(input).ShouldBe(answer);

    public string Solution1(string input)
    {
        var set = input.ReadAndSplitList();
        var list = new HashSet<int>();
        long total = 0;
        for (int y = 0; y < set.Length; y++)
        {
            var row = set[y];
            if (row.Contains('S'))
            {
                list.Add(Array.IndexOf(row, 'S'));
            }
            foreach ( var x in list.ToArray())
            {
                if (row[x] == '^')
                {
                    list.Remove(x);
                    list.Add(x - 1);
                    list.Add(x + 1);
                    total++;
                }
            }
        }

        return $"{total}";
    }

    public string Solution2(string input)
    {
        var set = input.ReadAndSplitList();
        var list = new Dictionary<int, long>();
        for (int y = 0; y < set.Length; y++)
        {
            var row = set[y];
            foreach (var x in list.ToArray())
            {
                if (row[x.Key] == '^')
                {
                    if (!list.ContainsKey(x.Key - 1))
                    {
                        list.Add(x.Key - 1, 0);
                    }
                    if (!list.ContainsKey(x.Key + 1))
                    {
                        list.Add(x.Key + 1, 0);
                    }
                    list[x.Key - 1] += list[x.Key];
                    list[x.Key + 1] += list[x.Key];
                    list.Remove(x.Key);
                }
            }
            if (row.Contains('S'))
            {
                list.Add(Array.IndexOf(row, 'S'), 1);
            }
#if DEBUG
            Console.WriteLine($"----------");
            Console.WriteLine($"y: {y}");
            foreach (var x in list.OrderBy(l => l.Key).ToArray())
            {
                Console.WriteLine($"{x.Key}:{x.Value}");
            }
#endif
        }

        return $"{list.Sum(l => l.Value)}";
    }
}
